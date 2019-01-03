using DSS.Data.Query.Enums;

namespace DSS.Data.Query.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterLeafNode : FilterNode
    {
        #region Properties

        /// <summary>
        /// The value of the filter node that will be used to filter
        /// a given filter column. This is a generic object so we can match multiple filter types.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// An object containing pre filled node filter data that can be used to populate filters.
        /// like select lists.
        /// </summary>
        public object PreFilterData { get; set; }

        /// <summary>
        /// Flag indicating if the filter representing a dropdown is populated on the client
        /// </summary>
        public bool IsDropdownClientPopulated { get; set; }

        /// <summary>
        /// The display property name for the PreFilterData in the case of a prefilter data collection.
        /// </summary>
        public string PreFilterDataDispaly { get; set; }

        /// <summary>
        /// The value property name for the PreFilterData in the case of a prefilter data collection.
        /// </summary>
        public string PreFilterDataValue { get; set; }
        
        /// <summary>
        /// The target data/entitiy column/property used to filter by the node.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The label used before the filter input UI element.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The operation type executed by the filter node.
        /// </summary>
        public FilterOperationType OperationType { get; set; }

        /// <summary>
        /// Describes the case sensitivity for certain string filter comparisons
        /// </summary>
        public FilterCaseSensitivity CaseSensitivity { get; set; }

        /// <summary>
        /// Determine the data type for the given filter node.
        /// </summary>
        public FilterDataType FilterDataType { get; set; }

        /// <summary>
        /// The type of the value object for the dropdown elements
        /// </summary>
        public DropdownValueDataType DropdownValueType { get; set; }

        #endregion

        #region Constructor

        public FilterLeafNode()
        {
            SetDefaults();
        }

        #endregion

        #region Privates
        
        /// <summary>
        /// Set default values for the filter leaf node
        /// </summary>
        private void SetDefaults()
        {
            Value = "";
            Target = "";
            Label = "";

            OperationType = FilterOperationType.Equal;
            Operator = BooleanOperator.And;
            FilterDataType = FilterDataType.String;
            CaseSensitivity = FilterCaseSensitivity.CaseInsensitive;

            DropdownValueType = DropdownValueDataType.Integer;
            
            PreFilterData = null;
            PreFilterDataDispaly = null;
            PreFilterDataValue = null;

            IsDropdownClientPopulated = false;
        }

        #endregion

        #region Static Constructio

        /// <summary>
        /// Creates an instance of a Filter Leaf Node which can be used to improve
        /// and use chainability during configuratoin
        /// </summary>
        /// <returns></returns>
        public static  FilterLeafNode Get()
        {
            return new FilterLeafNode();
        }

        #endregion
    }
}
