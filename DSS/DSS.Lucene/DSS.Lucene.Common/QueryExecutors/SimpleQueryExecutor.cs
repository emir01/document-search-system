using Lucene.Net.Index;
using Lucene.Net.Search;

namespace DSS.Lucene.Common.QueryExecutors
{
    public class SimpleQueryExecutor:ISimpleQueryExecutor
    {
        /// <summary>
        /// Execute the provided query using the index searcher on the index provided via the index reader.
        /// </summary>
        /// <param name="query">The query we are going to excecute on the index.</param>
        /// <param name="searcher">The searcher that will be used to execute the query.</param>
        /// <param name="reader">The reader that points us to the actual index</param>
        /// <returns><see cref="TopDocs"/> object containing the results of the executed query</returns>
        public TopDocs ExecuteQuery(Query query, IndexSearcher searcher, IndexReader reader)
        {
            // execute the query
            var results = searcher.Search(query, reader.MaxDoc);

            // return the results
            return results;
        }
    }
}
