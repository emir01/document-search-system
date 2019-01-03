using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace DSS.Lucene.Common.Queries
{
    /// <summary>
    /// Basic interface for creating Simple Single Field Queries on a single field.
    /// </summary>
    public class SingleFieldQueryFactory : ISingleFieldQueryFactory
    {
        /// <summary>
        /// Creates a single field query based on a passed in analyzer with the given field and search terms on the field.
        /// </summary>
        /// <param name="field">The field that will be queried in the index.</param>
        /// <param name="searchTerm">The search term for the field we are searching.</param>
        /// <param name="analyzer">The analyzer used to tokenzie the searching</param>
        /// <returns>A <see cref="Query"/> object on a single field.</returns>
        public Query GetSingleFieldQuery(string field, string searchTerm, Analyzer analyzer)
        {
            // Create a query parser on the specifided field using the specified version
            var queryParser = new QueryParser(Version.LUCENE_30, field, analyzer);

            // Create a query using the query parser
            var query = queryParser.Parse(searchTerm);

            // return the query
            return query;
        }
    }
}
