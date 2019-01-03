using System;
using System.Configuration;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Common.Utilities;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Entities;
using DSS.Lucene.Common.Entities;
using DSS.Lucene.Common.Entities.Enums;
using DSS.Lucene.Indexing.Services.Interface;
using DSS.Lucene.Tika.Interface;

namespace DSS.BusinessLogic.Common.Services
{
    /// <summary>
    /// The custom DSS related indexing service that relies on the LUCENE functionality to index its document and
    /// perform searches.
    /// </summary>
    public class DssIndexService : IDssIndexService
    {
        #region Dependencies

        /// <summary>
        /// The lucene utility/service used to store documents in an index.
        /// </summary>
        private readonly ILuceneIndexService<IndexedDocument> _luceneIndexService;

        /// <summary>
        /// The TIKA utility to extract text from documents
        /// </summary>
        private readonly ITextExtractor _textExtractor;

        /// <summary>
        /// The document repository used to talk to the document information stored in the database.
        /// </summary>
        private readonly IRepository<Document> _documentsRepository;

        #endregion

        #region Properties

        /// <summary>
        /// The path to the lucene index files.
        /// </summary>
        public string LuceneIndexPath { get; set; }

        #endregion

        #region Constructor

        public DssIndexService(ILuceneIndexService<IndexedDocument> luceneIndexService, ITextExtractor textExtractor, IRepository<Document> documentsRepository)
        {
            _luceneIndexService = luceneIndexService;
            _textExtractor = textExtractor;
            _documentsRepository = documentsRepository;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Toggle the document state in the search index.
        /// </summary>
        /// <param name="documentId"> </param>
        /// <returns>Base result containing the results of the operation</returns>
        public DataResult<IndexedDocument> AddDocumentToIndex(Guid documentId)
        {
            var result = new DataResult<IndexedDocument>();

            try
            {
                // use the repository to extarct the document for an uploaded document
                var document = _documentsRepository.Read(documentId);

                // create a lucene document from the given document
                var luceneDocument = GetLuceneDocumentFromDocument(document, true);

                // if there is no such document
                if (document == null)
                {
                    result.SetFailiure("There is no document with the selected document id");
                    return result;
                }

                // check if the document is already indexed
                if (document.IsIndexed)
                {
                    result.SetSuccess("Document with the selected id is already indexed");
                    result.SetData(luceneDocument);

                    return result;
                }

                var luceneIndexLocation = GetLuceneIndexLocation();

                // add it to the index
                var addToIndexResult = _luceneIndexService.AddToIndex(luceneIndexLocation, luceneDocument);

                if (addToIndexResult.GetStatus() == LuceneIndexingStatus.Success)
                {
                    SetDocumentIndexStatus(document, true);

                    // set success properties on the result
                    result.SetSuccess("Successs in adding the document to the lucene index");
                    result.SetData(luceneDocument);
                }
                else
                {
                    result.SetFailiure("Failed to add document to index");
                }
            }
            catch (Exception ex)
            {
                result.SetException(ex, "Exception while adding document to the lucene index");
            }

            return result;
        }

        /// <summary>
        /// Remove a document from the Lucene indexing, making it unavaiable for searching of general user 
        /// downloading
        /// </summary>
        /// <param name="documentId">The id of the document to be removed from the index</param>
        /// <returns>Data result containing document objet that was just removed from the index</returns>
        public DataResult<IndexedDocument> RemoveDocumentFromIndex(Guid documentId)
        {
            var result = new DataResult<IndexedDocument>();

            try
            {
                var document = _documentsRepository.Read(documentId);
                var luceneDocument = GetLuceneDocumentFromDocument(document);

                if (document == null)
                {
                    result.SetFailiure("There is no document with the given document id to remove from the index");
                    return result;
                }

                if (!document.IsIndexed)
                {
                    result.SetSuccess("Document is already removed from the index");
                    result.SetData(luceneDocument);
                    return result;
                }

                var lucenePath = GetLuceneIndexLocation();

                // try and remove the document from the index
                var removalResult = _luceneIndexService.RemoveFromIndex(lucenePath, luceneDocument, true);

                if (removalResult.GetStatus() == LuceneIndexingStatus.Success)
                {
                    SetDocumentIndexStatus(document, false);

                    result.SetSuccess("Document Successfully removed from index");
                    result.SetData(luceneDocument);
                }
                else
                {
                    result.SetFailiure("Failed to remove document from index");
                }

                return result;
            }
            catch (Exception ex)
            {
                result.SetException(ex, "Exception while trying to remove document from index");
                return result;
            }
        }

        /// <summary>
        /// Query the index used by the DSS Services for the number of indexed documents
        /// </summary>
        /// <returns>Data Result wrapping the number of indexed documents</returns>
        public DataResult<int> GetNumberOfIndexedDocuments()
        {
            var result = new DataResult<int>();

            try
            {
                var directoryPaty = GetLuceneIndexLocation();

                var luceneResult = _luceneIndexService.GetDocumentsNumber(directoryPaty);

                if(luceneResult.GetStatus() == LuceneIndexingStatus.Success)
                {
                    result.SetSuccess("Succeeded in retrieving the lucene documents number");
                    result.SetData(luceneResult.ResponseObject);
                }
                else
                {
                    result.SetFailiure("Failed to retrieve lucene index documents number");
                }
            }
            catch(Exception ex)
            {
                result.SetException(ex,"Exception while checking the number of indexed documents");
            }

            return result;
        }

        /// <summary>
        /// Check and return the flat for the optimization state regarding the index used by 
        /// </summary>
        /// <returns></returns>
        public DataResult<bool> IsIndexOptimized()
        {
            var result = new DataResult<bool>();

            try
            {
                var path = GetLuceneIndexLocation();
                var luceneResult = _luceneIndexService.IsIndexedOptimized(path);

                if(luceneResult.GetStatus() == LuceneIndexingStatus.Success)
                {
                    result.SetSuccess("Retrieved index optimization state");
                    result.SetData(luceneResult.ResponseObject);
                }
                else
                {
                    result.SetFailiure("Could not get lucene index optimization status");
                }
            }
            catch(Exception ex)
            {
                result.SetException(ex,"Exception while checking index optimize status");
            }

            return result;
        }

        /// <summary>
        /// Set the location of the lucene index based on the server path.
        /// </summary>
        public void SetIndexLocation(string location)
        {
            LuceneIndexPath = location;
        }

        #endregion

        #region Private Utilities

        /// <summary>
        /// Extract and create a lucene document object from the document entitiy object. By default will not
        /// perform text extraction
        /// </summary>
        /// <param name="document">The entitiy document object</param>
        /// <param name="extractText">Boolean flag indicating if text extraction should occur </param>
        /// <returns><see cref="IndexedDocument"/> object extracted from the entitiy document object</returns>
        private IndexedDocument GetLuceneDocumentFromDocument(Document document, bool extractText = false)
        {
            var luceneDocument = new IndexedDocument();

            luceneDocument.Title = document.Title;

            // we are going to set the id the same as on the uploaded document
            // so we can easiliy map the lucene results to documents in our system
            luceneDocument.Id = document.Id;

            if (extractText)
            {
                //get the file contents using the tika extractor
                var textExtractResult = _textExtractor.Extract(document.Path);

                luceneDocument.Contents = textExtractResult.Text;
            }
            else
            {
                luceneDocument.Contents = null;
            }

            // set the dates
            luceneDocument.DateIndexed = DateTime.Now;

            // set the date Created for the lucene document that corresponds to the Date the document
            // was uploaded in the system
            luceneDocument.DateCreated = document.DateUploaded;

            // return the lucene document
            return luceneDocument;
        }

        /// <summary>
        /// Returns the lucene index location.
        /// </summary>
        /// <returns>String path to the lucene index location.</returns>
        private string GetLuceneIndexLocation()
        {
            // get the concrete full path from the configuration
            var luceneIndexLocation = ConfigurationManager.AppSettings[ConfigurationSettingKeys.LuceneIndexLocation];

            // if the lucene index path is not set we are going to return 
            // a full path retrieved from the configuration
            if (string.IsNullOrWhiteSpace(LuceneIndexPath))
            {
                return luceneIndexLocation;
            }
            else
            {
                // otherwise retrieve the lucene server maped path set from a controller
                return LuceneIndexPath;
            }
        }

        /// <summary>
        /// Set the document index status in the database
        /// </summary>
        /// <param name="document">The document that will be set as indexed</param>
        /// <param name="indexStatus">The new index status in the app db</param>
        private void SetDocumentIndexStatus(Document document, bool indexStatus)
        {
            // update document index information
            document.IsIndexed = indexStatus;
            _documentsRepository.Update(document);
        }

        #endregion
    }
}
