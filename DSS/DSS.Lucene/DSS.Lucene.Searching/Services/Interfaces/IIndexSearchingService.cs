using DSS.Lucene.Common.Analyzers;
using DSS.Lucene.Common.Directories;
using DSS.Lucene.Searching.Entities.SearchResultObjects;
using Lucene.Net.Analysis;
using Lucene.Net.Store;

namespace DSS.Lucene.Searching.Services.Interfaces
{
    /// <summary>
    /// Describes a search service that provides query functionality for Lucene indexes.
    /// </summary>
    public interface IIndexSearchingService
    {
        #region Search

        SingleFieldSearchResult SingleFieldSearch(string fieldName, string query, string directoryPath);

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
