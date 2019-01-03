using System.Linq;

namespace DSS.Data.Query.QueryProcessObjects
{
    /// <summary>
    /// The basic Generic Query Result object containing the processed queryable collection
    /// returned from the query processing as well as Count Information
    /// </summary>
    public class QueryResult<T>
    {
        /// <summary>
        /// The processed data queryable collection
        /// </summary>
        public IQueryable<T> ProcessedData { get; set; }

        /// <summary>
        /// The total data entitiy collection count without any applied 
        /// filters or paging
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The number of data objects returned after filtering has been applied.
        /// </summary>
        public int FilteredCount { get; set; }
    }
}
