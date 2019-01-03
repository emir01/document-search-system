using DSS.Data.Query.Filters;

namespace DSS.Data.Query.QueryProcessors.Interface
{
    /// <summary>
    /// Describes an interface that Builds objects used to dynamicly query and filter data based on
    /// the filter nodes filtering options
    /// 
    /// The Dynamic filter type is based on the dynamic type T. 
    /// 
    /// It can currently be :
    ///     String - To be used with Dynamic Linq
    ///     Expressions - Not currently implemented 
    /// </summary>
    public interface IDynamicFilterBuilder<out T>
    {
        /// <summary>
        /// Return a dynamic filter object from the Dropdown filter leaf node
        /// </summary>
        /// <param name="filterLeafNode"></param>
        /// <returns>Object of type T that wraps the Dynamic Filter</returns>
        T GetDropdownDynamicFilter(FilterLeafNode filterLeafNode);

        /// <summary>
        /// Get a dynamic filter object from the String filter leaf node
        /// </summary>
        /// <param name="filterLeafNode"></param>
        /// <returns>Object of type T that wraps the Dynamic Filter</returns>
        T GetStringDynamicFilter(FilterLeafNode filterLeafNode);
    }
}
