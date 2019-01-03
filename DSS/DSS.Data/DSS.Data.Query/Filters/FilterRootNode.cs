using System.Collections.Generic;

namespace DSS.Data.Query.Filters
{
    /// <summary>
    /// A filter root object is used to combine multiple filter nodes under a given
    /// boolean operator.
    /// 
    /// Root nodes do not have a visual representation and are always only used to group a collection
    /// of leaf nodes under a common bollean And/Or operator.
    /// </summary>
    public class FilterRootNode : FilterNode
    {
        /// <summary>
        /// The Child Filters for the current filter root node.
        /// </summary>
        public List<FilterNode> Nodes { get; set; }
    }
}
