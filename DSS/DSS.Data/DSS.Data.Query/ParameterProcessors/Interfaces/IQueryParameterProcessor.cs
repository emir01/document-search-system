using DSS.Data.Query.FilterScaffolding;
using DSS.Data.Query.QueryProcessObjects;

namespace DSS.Data.Query.ParameterProcessors.Interfaces
{
    /// <summary>
    /// A Query paramater processor object for the JQueryDataTableParams functionality.
    /// </summary>
    /// <typeparam name="T">The general query parameter object type</typeparam>
    /// <typeparam name="TE">The actual type of the Entitiy for which the query is beeing processed</typeparam>
    public interface IQueryParameterProcessor<in T, TE>
    {
        /// <summary>
        /// Transforms a raw parameters objects for basic query params of type T as well as optional 
        /// Filter Scaffold Filter params of type TK to the general used Query Parameters object
        ///  </summary>
        /// <param name="generalQueryParameters"> The General Query Parameters describing general query properties( paging, sorting)</param>
        /// <param name="filterScaffoldModel">The Filter Scaffold model providing the filtering information for the Query Paramters object </param>
        /// <returns><see cref="QueryParameters"/> object that can be used by the main query processor</returns>
        QueryParameters ProcessParameters(T generalQueryParameters, FilterScaffoldModel<TE> filterScaffoldModel = null);
    }
}
