using System.Linq;

namespace DSS.Data.Query.QueryDataSources
{
    /// <summary>
    /// Describes an interface for a Grid Processor Data Source for a given entitiy T
    /// </summary>
    public interface IQueryDataSource<out T>
    {
        /// <summary>
        /// Return a queryable collection for T
        /// </summary>
        /// <returns>IQueryable collection over T</returns>
        IQueryable<T> GetQueryable();
    }
}
