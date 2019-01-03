using DSS.Data.Query.Enums;
using DSS.Data.Query.Filters;

namespace DSS.Data.Query.FilterScaffolding
{
    /// <summary>
    /// Contains extensions to the filter leaf node objects used in the
    /// Query Filtering sub module.
    /// </summary>
    public static class LeafNodeExtensions
    {
        #region Construct Target

        /// <summary>
        /// Set the Target property returning the node providing chainability for furhter configuration
        /// </summary>
        /// <param name="node"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static FilterLeafNode SetTarget(this FilterLeafNode node, string target)
        {
            node.Target = target;
            return node;
        }

        #endregion

        #region Construct Name

        /// <summary>
        /// Set the filter name, returning the node to provide fluid chainability
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FilterLeafNode SetName(this FilterLeafNode node, string name)
        {
            node.NodeName = name;
            return node;
        }

        #endregion

        #region Construct Id

        /// <summary>
        /// Set the filter id, returning the node to provide fluid chainability
        /// </summary>
        /// <param name="node"> The node we are currently working on</param>
        /// <param name="nodeId"> The </param>
        /// <returns></returns>
        public static FilterLeafNode SetId(this FilterLeafNode node, int nodeId)
        {
            node.NodeId = nodeId;
            return node;
        }

        #endregion

        #region Construct Label

        /// <summary>
        /// Set the Label property returning the Leaf node providing chainability
        /// </summary>
        /// <param name="node"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static FilterLeafNode SetLabel(this FilterLeafNode node, string label)
        {
            node.Label = label;
            return node;
        }

        #endregion

        #region Construction Operation Types

        /// <summary>
        /// Set the operation type returning the node providing chainability
        /// </summary>
        /// <param name="node"></param>
        /// <param name="operatoinType"></param>
        /// <returns></returns>
        public static FilterLeafNode SetOperationType(this FilterLeafNode node, FilterOperationType operatoinType)
        {
            node.OperationType = operatoinType;
            return node;
        }

        #endregion

        #region Construct Case sensitivity

        /// <summary>
        /// Set the node comparison to Case Sensitive checks returning the node to provide
        /// chainability
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static FilterLeafNode SetCaseSensitive(this FilterLeafNode node)
        {
            node.CaseSensitivity = FilterCaseSensitivity.CaseSensitive;
            return node;
        }

        /// <summary>
        /// Make the filter node checks case Insensitive returning the node to provide chainability 
        /// during configuration
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static FilterLeafNode SetCaseInsensitive(this FilterLeafNode node)
        {
            node.CaseSensitivity = FilterCaseSensitivity.CaseInsensitive;
            return node;
        }

        #endregion

        #region Construct Data Types
        
        /// <summary>
        /// Set the filter data type returning the node providing chainability during configuration
        /// </summary>
        /// <param name="node"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static FilterLeafNode SetDataType(this FilterLeafNode node, FilterDataType dataType)
        {
            node.FilterDataType = dataType;
            return node;
        }

        #endregion

        #region Construct Boolean Operator
        
        /// <summary>
        /// Set the boolean comparison merger to And, and return the node to provide chainability
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static FilterLeafNode SetAnd(this FilterLeafNode node)
        {
            node.Operator = BooleanOperator.And;
            return node;
        }

        /// <summary>
        /// Set the boolean comparison merger to Or, and return the node to provide chainability
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static FilterLeafNode SetOr(this FilterLeafNode node)
        {
            node.Operator = BooleanOperator.Or;
            return node;
        }

        #endregion

        #region Dropdown Settings
        
        /// <summary>
        /// If this filter node represents a dropdown input element this flag sets the filter node to be 
        /// prepopulated on the server
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static FilterLeafNode SetServerDropdown(this FilterLeafNode node)
        {
            node.IsDropdownClientPopulated = false;
            return node;
        }

        /// <summary>
        /// Sets the client population flag for a dropdown filter. The dropdown will be preprocessed on populated on the client
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static FilterLeafNode SetClientDropdown(this FilterLeafNode node)
        {
            node.IsDropdownClientPopulated = true;
            return node;
        }

        /// <summary>
        /// Set the dropdown value type for the dropdown filters
        /// </summary>
        /// <param name="node"></param>
        /// <param name="dropDownValueType"></param>
        /// <returns></returns>
        public static FilterLeafNode SetDropdownValueType(this FilterLeafNode node,  DropdownValueDataType dropDownValueType)
        {
            node.DropdownValueType = dropDownValueType;

            return node;
        }

        #endregion

        #region Construct Pre filter Data

        /// <summary>
        /// Set the prefilter data for the filter node that can be used .
        /// </summary>
        /// <param name="leafNode"></param>
        /// <param name="preFilterData">The  pre loaded data to be used by the filter during the UI construction process.</param>
        /// <param name="displaySelector"> The optional name of  the property on the prefilter data to be used as the display element the UI/</param>
        /// <param name="valueSelector"> The optional name of the property on the prefilter data to be used as the value element during filter selection</param>
        /// <returns></returns>
        public static FilterLeafNode SetPrefilterData(this FilterLeafNode leafNode, object preFilterData, string displaySelector = "", string valueSelector = "")
        {
            leafNode.PreFilterData = preFilterData;

            if (!string.IsNullOrEmpty(displaySelector))
            {
                leafNode.PreFilterDataDispaly = displaySelector;
            }

            if (!string.IsNullOrEmpty(valueSelector))
            {
                leafNode.PreFilterDataValue = valueSelector;
            }

            return leafNode;
        }

        /// <summary>
        /// Set the dispaly property name for the prefilter data object/collection
        /// </summary>
        /// <param name="leafNode">The node for which we are setting the values</param>
        /// <param name="displaySelector">The name of the display property on the prefilter data to use on the UI</param>
        /// <returns></returns>
        public static FilterLeafNode SetPrefilterDisplayName (this FilterLeafNode leafNode, string displaySelector)
        {
            leafNode.PreFilterDataDispaly = displaySelector;
            return leafNode;
        }

        /// <summary>
        /// Set the value property name for the prefilter data object/collection
        /// </summary>
        /// <param name="leafNode"></param>
        /// <param name="valueSelector">The name of the property on the prefilter data to be used as the value selected on the UI </param>
        /// <returns></returns>
        public static FilterLeafNode SetPrefilterValueName(this FilterLeafNode leafNode, string valueSelector)
        {
            leafNode.PreFilterDataValue = valueSelector;
            return leafNode;
        }

        #endregion
    }
}
