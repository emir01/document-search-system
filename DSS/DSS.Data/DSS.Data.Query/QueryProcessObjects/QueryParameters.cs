using System.Collections.Generic;
using DSS.Data.Query.Filters;
using DSS.Data.Query.Sorting;

namespace DSS.Data.Query.QueryProcessObjects
{
    /// <summary>
    /// The Grid Processor Parameters object.
    /// 
    /// This is a collection of generic parameters that will be mapped from client provided objects
    /// provided objects
    /// </summary>
    public class QueryParameters
    {
        #region Constructor

        public QueryParameters()
        {
            SortNodes = new List<SortNode>();
            Filters = new List<FilterNode>();
        }

        #endregion

        #region Query Properties

        /// <summary>
        /// The number of records to skip the fitering and sort process. Usefully if we want to display the 
        /// queried data in a grid structure using paging.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The number or records to take after skipping a certain amount of records.
        /// Used in situations where the data is to be displayed in a grid format.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Collection of filters to be executed for the current grid processing query
        /// </summary>
        public List<FilterNode> Filters { get; set; }

        /// <summary>
        /// A collection of sorting notes used to sort the data returned by the query.
        /// Each sort node has a priority flag determining the priority with which it should be applied to the
        /// returned data set.
        /// </summary>
        public List<SortNode> SortNodes { get; set; }
        
        #endregion
    }
}
