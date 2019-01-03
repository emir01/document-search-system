using DSS.Data.Query.Enums;

namespace DSS.Data.Query.Filters
{
    /// <summary>
    /// Describe an abstract filter object containing common filter object properties
    /// shared by both root filter objects and noode filter objects
    /// </summary>
    public abstract class FilterNode
    {
        /// <summary>
        /// Internal referential id for the current filter node.
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// A descriptive node name for the current filter node.
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// Each filter node links to other nodes on its level with the given boolean operator
        /// </summary>
        public BooleanOperator Operator { get; set; }
    }
}
