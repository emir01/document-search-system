using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Common.Utilities;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Services
{
    public class DocumentsService: IDocumentsService
    {
        #region Properties

        /// <summary>
        /// The document repository used for document data access
        /// </summary>
        private readonly IRepository<Document> _documentRepository;

        private readonly IRepository<Category> _categoryRepository;

        private readonly IRepository<Keyword> _keywordRepository;

        private readonly IUserRepository _userRepository;

        #endregion

        #region Cached Values

        /// <summary>
        /// The file paths might be accessed multiple times in the lifetime of 
        /// the service so it makes sense to cache these  using this basic method
        /// </summary>
        private IEnumerable<string> _cacheDocumentFilePaths;

        #endregion

        #region Constructor

        public DocumentsService(IRepository<Document> documentRepository, IRepository<Category> categoryRepository, IRepository<Keyword> keywordRepository, IUserRepository userRepository)
        {
            _documentRepository = documentRepository;
            _categoryRepository = categoryRepository;
            _keywordRepository = keywordRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Return all the documents in the system.
        /// </summary>
        /// <returns>Data result wrapper for the documents in the system.</returns>
        public DataResult<IList<Document>> GetDocuments()
        {
            var result = ResultFactory.GetDataResult<IList<Document>>();

            try
            {
                var allDocuments = _documentRepository.ReadAll().ToList();

                result.SetData(allDocuments);
                result.SetSuccess("Successfuly retrieved all the documents");
            }
            catch (Exception ex)
            {
                result.SetException(ex, "Exception when retrieving documents");
            }

            return result;
        }

        /// <summary>
        /// Return a single document entitiy for the given document id
        /// </summary>
        /// <param name="id">The id of the document we want to retrieve</param>
        /// <returns>Data Result containing the document id</returns>
        public DataResult<Document> GetDocumentById(Guid id)
        {
            var result = ResultFactory.GetDataResult<Document>();

            try
            {
                var document = _documentRepository.Read(id);

                // if the document is null the business logic
                // makes this a failed atempt to retrieve the document
                if(document == null)
                {
                    result.SetFailiure("There is no document with the selected id");
                }
                else
                {
                    result.SetData(document);
                    result.SetSuccess("Succesfuly retrieved a document with the given id");
                }
            }
            catch(Exception ex)
            {
                result.SetException(ex,"Exception while trying to retrieve a single document by id");
                result.Message = "Error while retrieving document information. Please try again";
            }

            return result;
        }

        /// <summary>
        /// Store the newly uploaded documents. Initially process the categories and keywords for the documents.
        /// </summary>
        /// <param name="documets">The list of document entities that will be saved in the DSS System</param>
        /// <param name="loggedUserName"> The username for the user that uploaded and created the documents</param>
        /// <param name="uniqueCategories"> The list of unique categories in the current file upload request</param>
        /// <param name="uniqueKeywords">The list of unique keywords in the current file upload request </param>
        /// <returns></returns>
        public BaseResult StoreDocuments(IList<Document> documets, string loggedUserName, List<Category> uniqueCategories, List<Keyword> uniqueKeywords)
        {
            // Create a result object
            var result = ResultFactory.GetResultObject();

            try
            {
                // get all the already stored keywords in the system
                var existingKeywords = _keywordRepository.ReadAll().ToList();

                // get the list of all categories
                var existingCategories = _categoryRepository.ReadAll().ToList();

                // process and store all the new keywords that are to be created.
                // processed keywords should now contain objects that are atached to the context as not-modified.
                // as they were either added or used from the existing array
                var processedKeywords = ProcessAndStoreNewKeywords(uniqueKeywords, existingKeywords);

                // store the documents on by one
                foreach (var document in documets)
                {
                    // update the keywords based on the processed keywords that are all added to the context/db
                    UpdateKeywordsForDocument(document, processedKeywords);

                    // update the categories based on the existing categories
                    UpdateCategoriesForDocument(document, existingCategories);

                    // Set document user
                    var user = _userRepository.ReadAllAsQueryable().Where(x => x.Username == loggedUserName).ToList().FirstOrDefault();

                    if (user == null)
                    {
                        throw new ApplicationException("Could not find user with given username");
                    }

                    document.User = user;

                    // add the document to the context.
                    // At this point it should be added referencing keyword and category objects that are atached
                    // to the context with the unmofied state.
                    _documentRepository.CreateWithNoSave(document);
                }

                // after adding all the documents to the context without saving
                // call the context save changes action
                _documentRepository.SaveChanges();

                // If we add all the documents
                // set the result to success
                result.SetSuccess("Successfuly created all the document entities.");
            }
            catch(Exception ex)
            {
                result.SetException(ex,"Exception on the Document Service when storing documents");
            }

            return result;
        }


        /// <summary>
        /// Check if a document already is in the system based on the name of the file.
        /// </summary>
        /// <param name="filePath">
        /// The name of the document file which will be used to check if the document
        /// is in the system. Represented by the path the file will be saved on the server
        /// </param>
        /// <returns>DataResult object containing a boolean value indicating if the document is saved in the system.</returns>
        public DataResult<bool> IsDocumentInSystem(string filePath)
        {
            var result = ResultFactory.GetDataResult<bool>();
            
            try
            {
                // Check if we already have read all the document file paths and 
                // if the result is in the cache property
                if(_cacheDocumentFilePaths == null)
                {
                    _cacheDocumentFilePaths = _documentRepository.ReadAll().Select(x => x.Path.ToLower());
                }

                var allFilePaths = _cacheDocumentFilePaths;

                // If there is a file path with the same value as the path we are checking
                // then we will return false
                if(allFilePaths.Contains(filePath.ToLower()))
                {
                    result.SetSuccess("The file path has been found in the stored documents.");
                    result.SetData(true);
                }
                else
                {
                    result.SetSuccess("The file has not been found in the stored documents");
                    result.SetData(false);
                }
            }
            catch (Exception ex)
            {
                result.SetException(ex,"Exception occured on Document Service");
            }

            return result;
        }

        /// <summary>
        /// Update the information for a given document stored in the syste.
        /// </summary>
        /// <param name="documentData">The updated document information</param>
        /// <returns>Data Result containing the updated document information.</returns>
        public DataResult<Document> UpdateDocument(Document documentData)
        {
            var result = new DataResult<Document>();

            try
            {
                var document  = _documentRepository.Update(documentData);

                result.SetSuccess("Successfully updated document data");
                result.SetData(document);
            }
            catch(Exception ex)
            {
                result.SetException(ex,"Exceptin occured while updating document information");
                result.SetException(ex,"Exceptin occured while updating document information");
            }

            return result;
        }

        #endregion

        #region Private Methods

        #region Document Upload
        
        /// <summary>
        /// Updates the keyword references for a new document using the list of processed keywords.
        /// 
        /// This is used to prevent issues with doubly creating keywords. 
        /// </summary>
        /// <param name="newDocument"></param>
        /// <param name="processedKeywords">Processed keywords should contain all existing and new keywords atached to the context</param>
        private void UpdateKeywordsForDocument(Document newDocument, List<Keyword> processedKeywords)
        {
            // if the new document doesnt have any keywords just return
            if (newDocument.Keywords == null)
            {
                return;
            }

            for (int i = 0; i < newDocument.Keywords.Count; i++)
            {
                // get the keyword on the document
                var documentKeyword = newDocument.Keywords[i];

                // find the matching processed keyword on the list of processed keywords
                // based on the keyword name
                var processedKeyword =
                    processedKeywords.FirstOrDefault(proc => proc.Name.ToLower() == documentKeyword.Name.ToLower());

                // if we find a processed keyword update the document reference.
                if (processedKeyword != null)
                {
                    newDocument.Keywords[i] = processedKeyword;
                }
            }
        }

        /// <summary>
        /// Update the category refernces for a given document with the context added unchanged objects.
        /// 
        /// This is used to prevent creating new category confilicts with objects not atached to the context
        /// </summary>
        /// <param name="newDocument"></param>
        /// <param name="existingCategories">Should contain all the existing categories atached to the context</param>
        private void UpdateCategoriesForDocument(Document newDocument, List<Category> existingCategories)
        {
            // if the new document doesnt have any categories
            // just return
            if (newDocument.Categories == null)
            {
                return;
            }
            
            for (int i = 0; i < newDocument.Categories.Count; i++)
            {
                var documentCategory = newDocument.Categories[i];

                // find the category in the array of existing categories
                var existingCategory = existingCategories.FirstOrDefault(existing => existing.Id == documentCategory.Id);

                if (existingCategory != null)
                {
                    newDocument.Categories[i] = existingCategory;
                }
            }
        }
        
        /// <summary>
        /// Process the list of unique upoaded keyword
        /// </summary>
        /// <param name="uniqueKeywords"></param>
        /// <param name="existingKeywords"></param>
        /// <returns></returns>
        private List<Keyword> ProcessAndStoreNewKeywords(IEnumerable<Keyword> uniqueKeywords, List<Keyword> existingKeywords)
        {
            // The list of all processed keyword objects
            var processedKeywords = new List<Keyword>();

            // for all the "uploaded" keywords check if they already exsist
            foreach (var newKeyword in uniqueKeywords)
            {
                // If the new keyword already exsists
                if (existingKeywords.Any(existing => existing.Name.ToLower() == newKeyword.Name.ToLower()))
                {
                    // get the exsisting keyword
                    var existingKeyword = existingKeywords.FirstOrDefault(x => x.Name.ToLower() == newKeyword.Name.ToLower());

                    // if we find the exsiting keyword we will add it to the processed array
                    // and use it as a reference for the documents
                    if (existingKeyword != null) processedKeywords.Add(existingKeyword);
                }
                else
                {
                    // if the new keyword does not exsist we are going to add it to the context without saving
                    newKeyword.Id = Guid.NewGuid();
                    newKeyword.Name = newKeyword.Name.ToLower();
                    newKeyword.Alias = newKeyword.Name.ToLower();

                    var storedKeyword = _keywordRepository.CreateWithNoSave(newKeyword);

                    processedKeywords.Add(storedKeyword);
                }
            }

            // call save changes on the context
            _keywordRepository.SaveChanges();

            return processedKeywords;
        }

        #endregion

        #endregion
    }
}