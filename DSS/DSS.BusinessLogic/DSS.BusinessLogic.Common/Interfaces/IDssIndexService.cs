using System;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Lucene.Common.Entities;

namespace DSS.BusinessLogic.Common.Interfaces
{
    /// <summary>
    /// The custom DSS related indexing service that relies on the LUCENE functionality to index its document and
    /// perform searches.
    /// </summary>
    public interface IDssIndexService
    {
        /// <summary>
        /// The path to the lucene index files.
        /// </summary>
        string LuceneIndexPath { get; set; }

        /// <summary>
        /// Add an already uploaded document to the Lucene index.
        /// </summary>
        /// <param name="documentId">The id of the uploaded document that will be added to the lucene index</param>
        /// <returns>Data Result containing the lucene document for the given entity document</returns>
        DataResult<IndexedDocument> AddDocumentToIndex(Guid documentId);

        /// <summary>
        /// Remove a document from the Lucene indexing, making it unavaiable for searching of general user 
        /// downloading
        /// </summary>
        /// <param name="documentId">The id of the document to be removed from the index</param>
        /// <returns>Data result containing document objet that was just removed from the index</returns>
        DataResult<IndexedDocument> RemoveDocumentFromIndex(Guid documentId);

        /// <summary>
        /// Query the index used by the DSS Services for the number of indexed documents
        /// </summary>
        /// <returns>Data Result wrapping the number of indexed documents</returns>
        DataResult<int> GetNumberOfIndexedDocuments();

        /// <summary>
        /// Check and return the flat for the optimization state regarding the index used by 
        /// </summary>
        /// <returns></returns>
        DataResult<bool> IsIndexOptimized();

        /// <summary>
        /// Set the location of the lucene index based on the server path.
        /// </summary>
        void SetIndexLocation(string location);
    }
}
