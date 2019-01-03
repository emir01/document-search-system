using Lucene.Net.Analysis;
using Lucene.Net.Search;

namespace DSS.Lucene.Common.Queries
{
    /// <summary>
    /// Basic interface for creating Simple Single Field Queries on a single field on a Lucene index.
    /// </summary>
    public interface ISingleFieldQueryFactory
    {
        /// <summary>
        /// Creates a single field query based on a passed in analyzer with the given field and search terms on the field.
        /// </summary>
        /// <param name="field">The field that will be queried in the index.</param>
        /// <param name="searchTerm">The search term for the field we are searching.</param>
        /// <param name="analyzer">The analyzer used to tokenzie the searching</param>
        /// <returns>A <see cref="Query"/> object on a single field.</returns>
        Query GetSingleFieldQuery(string field, string searchTerm, Analyzer analyzer);
    }
}
