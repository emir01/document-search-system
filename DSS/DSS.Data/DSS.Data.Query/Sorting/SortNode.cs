using DSS.Data.Query.Enums;

namespace DSS.Data.Query.Sorting
{
    /// <summary>
    /// The generic sort node object, containing the column/property data value upon which to sort 
    /// and sorting direction as well as sort priority
    /// </summary>
    public class SortNode
    {
        /// <summary>
        /// Defines the direction of the sorting process.
        /// </summary>
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Define the column/property data target for the sorting operation
        /// </summary>
        public string SortTarget { get; set; }

        /// <summary>
        /// Used in possible multiple sort scenarios to define the sorting order.
        /// </summary>
        public int SortPriority { get; set; }
    }
}
