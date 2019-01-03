using DSS.Lucene.Common.Analyzers;
using DSS.Lucene.Common.Directories;
using DSS.Lucene.Common.Entities;
using Lucene.Net.Analysis;
using Lucene.Net.Store;

namespace DSS.Lucene.Indexing.Services.Interface
{
    /// <summary>
    /// Describes a basic indexing service used to index documents stored in objects with type T
    /// </summary>
    /// <typeparam name="T">The type of </typeparam>
    public interface ILuceneIndexService<in T>
    {
        #region Index Actions

        /// <summary>
        /// Adds a data object of type T to the index
        /// </summary>
        /// <param name="indexDirectoryPath">The directoryFactory path where the index is located.</param>
        /// <param name="data">The data object that will be stored in the index</param>
        /// <returns>Boolean value wrapped result indicating the success of the operation.</returns>
        IndexQueryResult<bool> AddToIndex(string indexDirectoryPath, T data);

        /// <summary>
        /// Remove the object described with the data ttribute from the Lucene Index.
        /// </summary>
        /// <param name="indexDirectoryPath">The path to the Lucene Index directory</param>
        /// <param name="data">The data representing the object to be removed from the index</param>
        /// <param name="optimzeOnRemoval">Boolean flag indicating if we want to optimze the index on removal</param>
        /// <returns>Boolean value wrapped result indicating the success of the operation</returns>
        IndexQueryResult<bool> RemoveFromIndex(string indexDirectoryPath, T data, bool optimzeOnRemoval);

        /// <summary>
        /// Call the raw optimze command on lower level index objects.
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index we will be optimizing</param>
        /// <param name="options"> Optional Index optimization parameters object. If null default options are used </param>
        /// <returns>Boolean value wrapped result  indicating the success of the operation</returns>
        IndexQueryResult<bool> OptimizeIndex(string indexDirectoryPath, IndexOptimizeOptions options);

        /// <summary>
        /// Returns a boolean flag indicating if the index is optimized.
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index for which we will be checking optimization flags</param>
        /// <returns>Boolean value wrapped result indicating if the index is optimzed</returns>
        IndexQueryResult<bool> IsIndexedOptimized(string indexDirectoryPath);

        /// <summary>
        /// Returns the number of documents removed from the index
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index for which we will be checking number of deleted documents</param>
        /// <returns>Integer value wrapped result indicating the number of deleted documents</returns>
        IndexQueryResult<int > GetDeletedDocuments(string indexDirectoryPath);

        /// <summary>
        /// Get the total number of active documents in the index
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index for which we will be checking number of total indexed documents</param>
        /// <returns>Integer value wrapped result indicating the number of total indexe documents in the system</returns>
        IndexQueryResult<int> GetDocumentsNumber(string indexDirectoryPath);

        #endregion

        #region Factory Configuration

        /// <summary>
        /// Used to change the analyzerFactory used to create the analyzers in run-time depending
        /// on indexing requirments
        /// </summary>
        /// <param name="analyzerFactory">The analyzer analyzerFactory used in </param>
        void SetAnalyzerFactory(IAnalyzerFactory<Analyzer> analyzerFactory);

        /// <summary>
        /// Used to change the analyzerFactory used to create the lucene directories in run-time  depending on the indexing requirments.
        /// </summary>
        /// <param name="directoryFactory">The new directory factory.</param>
        void SetDirectoryFactory(IDirectoryFactory<Directory> directoryFactory);

        #endregion
    }
}
