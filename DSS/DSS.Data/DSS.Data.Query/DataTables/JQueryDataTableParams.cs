
using System.Collections.Generic;

namespace DSS.Data.Query.DataTables
{
    /// <summary>
    /// The generic JQuery Data Table Params object used by Default by DSS.Data.Query for interacting with Data Tables
    /// 
    /// The Default Data Query Processors are based over the JQuery Data Table Params Objects
    /// </summary>
    public class JQueryDataTableParams
    {
        // Single parameters
        public int sEcho { get; set; }

        public int iDisplayStart { get; set; }

        public int iDisplayLength { get; set; }

        public int iColumns { get; set; }
        
        public string sSearch { get; set; }

        public bool bEscapeRegex { get; set; }

        public int iSortingCols { get; set; }

        // List parameters

        public List<string> asSortDir { get; set; }
        
        public List<int> aiSortCol { get; set; }

        public List<bool> abSortable { get; set; }

        public List<bool> abSearchable { get; set; }

        public List<string> asSearchColumns { get; set; }

        public List<bool> abEscapeRegexColumns { get; set; }

        public List<string> asSearch { get; set; }

        /// <summary>
        /// The names of the properties on which the columns on the grid are bound in the given order.
        /// </summary>
        public List<string> amDataProp { get; set; }
    }
}
