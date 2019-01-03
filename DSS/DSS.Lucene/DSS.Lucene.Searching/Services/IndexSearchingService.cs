using DSS.Lucene.Common.Analyzers;
using DSS.Lucene.Common.Directories;
using DSS.Lucene.Common.IndexReaders;
using DSS.Lucene.Common.IndexSearchers;
using DSS.Lucene.Common.Queries;
using DSS.Lucene.Common.QueryExecutors;
using DSS.Lucene.Searching.Entities.SearchResultObjects;
using DSS.Lucene.Searching.Services.Interfaces;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace DSS.Lucene.Searching.Services
{
    public class IndexSearchingService : IIndexSearchingService
    {
        #region Properties

        private  IDirectoryFactory<Directory> _directoryFactory;
        
        private IAnalyzerFactory<Analyzer> _analyzerFactory;
        
        private readonly IIndexReaderFactory<IndexReader> _indexReaderFactorty;
        
        private readonly IIndexSearcherFactory<IndexSearcher> _indexSearcherFactory;
        
        private readonly ISingleFieldQueryFactory _singleFieldQueryFactory;
        
        private readonly ISimpleQueryExecutor _simpleQueryExecutor;

        #endregion

        #region Constructor

        public IndexSearchingService()
        {
            _directoryFactory = new DirectoryBaseFactory();
            _indexReaderFactorty = new IndexReaderBaseFactory();
            _indexSearcherFactory = new IndexSearcherBaseFactory();
            _singleFieldQueryFactory  = new SingleFieldQueryFactory();
            _simpleQueryExecutor = new SimpleQueryExecutor();
            _analyzerFactory = new AnalyzerBaseFactory();
        }

        #endregion 

        public SingleFieldSearchResult SingleFieldSearch(string fieldName, string query, string directoryPath)
        {
            // Create the index reader
            var directory = _directoryFactory.GetDirectory(directoryPath);
            var indexReader = _indexReaderFactorty.GetIndexReader(directory);

            // create a index searcher
            var searcher = _indexSearcherFactory.GetIndexSearcher(indexReader);

            // Create the query to be executed
            var contentQuery = _singleFieldQueryFactory.GetSingleFieldQuery(fieldName,
                                                                                 query,
                                                                                 _analyzerFactory.GetAnalyzer());

            // Execute the query.
            var results = _simpleQueryExecutor.ExecuteQuery(contentQuery, searcher, indexReader);

            // Create the SingleFieldSearchResul wrapper and return it as the result of the search
            var resultWrapper = new SingleFieldSearchResult(searcher) {TopDocs = results};
            return resultWrapper;
        }

        #region Factory Configuration
        /// <summary>
        /// Used to change the analyzerFactory used to create the analyzers in run-time depending
        /// on indexing requirments
        /// </summary>
        /// <param name="analyzerFactory">The analyzer analyzerFactory used in </param>
        public void SetAnalyzerFactory(IAnalyzerFactory<Analyzer> analyzerFactory)
        {
            _analyzerFactory = analyzerFactory;
        }

        /// <summary>
        /// Used to change the analyzerFactory used to create the lucene directories in run-time  depending on the indexing requirments.
        /// </summary>
        /// <param name="directoryFactory">The new directory factory.</param>
        public void SetDirectoryFactory(IDirectoryFactory<Directory> directoryFactory)
        {
            _directoryFactory = directoryFactory;
        }

        #endregion
    }
}