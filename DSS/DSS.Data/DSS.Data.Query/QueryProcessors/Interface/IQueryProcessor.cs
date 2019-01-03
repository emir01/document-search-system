using System.Linq;
using DSS.Data.Query.QueryProcessObjects;

namespace DSS.Data.Query.QueryProcessors.Interface
{
    /// <summary>
    /// Describes a grid request processor for a given entitiy.
    /// This is the main object in the Grid Processing functionality.
    /// 
    /// For a given type T it relies on the 
    /// </summary>
    /// <typeparam name="T">The main entitiy type which is processed</typeparam>
    public interface IQueryProcessor<T>
    {
        /// <summary>
        /// Returns a processed queryable object for a domain entitiy of type T, processed using the Query Parameters object
        /// </summary>
        /// <param name="queryable"> The input queryable that will be transformed and processed based on the Query Parameters </param>
        /// <param name="parameters">The Query parameter object</param>
        /// <returns></returns>
        QueryResult<T> ProcessQuery(IQueryable<T> queryable, QueryParameters parameters);
    }
}
