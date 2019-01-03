namespace DSS.Data.Query.DataTables
{
    /// <summary>
    /// The object tha represents a jquery data tables result which is used by default in 
    /// Data Query functionality.
    /// </summary>
    public class JQueryDataTableResult
    {
        public int sEcho { get; set; }

        public object aaData { get; set; }

        public int iTotalRecords { get; set; }

        public int iTotalDisplayRecords { get; set; }
    }
}