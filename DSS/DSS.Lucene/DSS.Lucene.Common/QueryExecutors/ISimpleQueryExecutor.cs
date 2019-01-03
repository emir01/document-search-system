using Lucene.Net.Index;
using Lucene.Net.Search;

namespace DSS.Lucene.Common.QueryExecutors
{
    /// <summary>
    /// Simple interface for executing queries on an index reader, using the provided searchers.
    /// </summary>
    public interface ISimpleQueryExecutor
    {
        /// <summary>
        /// Execute the provided query using the index searcher on the index provided via the index reader.
        /// </summary>
        /// <param name="query">The query we are going to excecute on the index.</param>
        /// <param name="searcher">The searcher that will be used to execute the query.</param>
        /// <param name="reader">The reader that points us to the actual index</param>
        /// <returns><see cref="TopDocs"/> object containing the results of the executed query</returns>
        TopDocs ExecuteQuery(Query query, IndexSearcher searcher, IndexReader reader);
    }
}
