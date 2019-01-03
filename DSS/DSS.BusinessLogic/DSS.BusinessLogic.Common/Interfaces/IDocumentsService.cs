using System;
using System.Collections.Generic;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Interfaces
{
    /// <summary>
    /// Interface describing the basic document management code.
    /// </summary>
    public interface IDocumentsService
    {
        /// <summary>
        /// Return all the documents in the system.
        /// </summary>
        /// <returns>Data result wrapper for the documents in the system.</returns>
        DataResult<IList<Document>> GetDocuments();

        /// <summary>
        /// Return a single document entitiy for the given document id
        /// </summary>
        /// <param name="id">The id of the document we want to retrieve</param>
        /// <returns>Data Result containing the document id</returns>
        DataResult<Document> GetDocumentById(Guid id);

        /// <summary>
        /// creates entiteis for the given documest.
        /// </summary>
        /// <param name="documets">The list of document entities that will be saved in the DSS System</param>
        /// <param name="loggedUserName"> The username for the user that uploaded and created the documents</param>
        /// <param name="uniqueCategories"> The list of unique categories in the current file upload request</param>
        /// <param name="uniqueKeywords">The list of unique keywords in the current file upload request </param>
        /// <returns></returns>
        BaseResult StoreDocuments(IList<Document> documets, string loggedUserName, List<Category> uniqueCategories, List<Keyword> uniqueKeywords);

        /// <summary>
        /// Check if a document already is in the system based on the name of the file.
        /// </summary>
        /// <param name="filePath">
        /// The name of the document file which will be used to check if the document
        /// is in the system
        /// </param>
        /// <returns>DataResult object containing a boolean value indicating if the document is saved in the system.</returns>
        DataResult<bool> IsDocumentInSystem(string filePath);

        /// <summary>
        /// Update the information for a given document stored in the syste.
        /// </summary>
        /// <param name="documentData">The updated document information</param>
        /// <returns>Data Result containing the updated document information.</returns>
        DataResult<Document> UpdateDocument(Document documentData);
    }
}
