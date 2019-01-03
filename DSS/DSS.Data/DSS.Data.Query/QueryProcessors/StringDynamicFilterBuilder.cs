using System;
using DSS.Data.Query.Enums;
using DSS.Data.Query.Filters;
using DSS.Data.Query.QueryProcessors.Interface;

namespace DSS.Data.Query.QueryProcessors
{
    /// <summary>
    /// Returns dynamic filter objects based on filter type in a string format
    /// to be used using Dynamic Linq.
    /// 
    /// </summary>
    public class StringDynamicFilterBuilder : IDynamicFilterBuilder<string>
    {
        #region Filter Building

        /// <summary>
        /// Return a dynamic filter object from the Dropdown filter leaf node
        /// </summary>
        /// <param name="filterLeafNode"></param>
        /// <returns>Object of type T that wraps the Dynamic Filter</returns>
        public string GetDropdownDynamicFilter(FilterLeafNode filterLeafNode)
        {
            var dynamicFilter = "";

            // try and validate the filter leaf node value
            object dropdownValue;

            var innerDataType = InnerDataFilterType.String;

            // switch on the dropdown value type
            switch (filterLeafNode.DropdownValueType)
            {
                case DropdownValueDataType.String:
                    dropdownValue = filterLeafNode.Value as string;
                    break;

                case DropdownValueDataType.Integer:
                    var parsedValue = GetIntegerFilterValue(filterLeafNode);
                    innerDataType = InnerDataFilterType.Number;

                    // for integer dropdowns if the value is -1 we are not going to continue
                    // with the filtering

                    if (parsedValue == -1)
                    {
                        return dynamicFilter;
                    }

                    dropdownValue = parsedValue;
                    break;

                case DropdownValueDataType.Boolean:

                    // as the boolean values are to be set as numbers on the dropdowns
                    // we are first goiung to get the parsed value
                    var parsedBooleanValue = GetIntegerFilterValue(filterLeafNode);

                    if (parsedBooleanValue == -1)
                    {
                        return dynamicFilter;
                    }

                    // Set the dropdown boolean value
                    dropdownValue = parsedBooleanValue == 1;

                    break;

                case DropdownValueDataType.Guid:
                    Guid parsedGuid;
                    Guid.TryParse(filterLeafNode.Value.ToString(), out parsedGuid);

                    dropdownValue = parsedGuid;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Build the actual dynamic filter string based on the filter node target
            var target = filterLeafNode.Target;

            dynamicFilter = string.Format("{0} {1} {2}", target, GetOperatorFromOperatorType(filterLeafNode.OperationType, innerDataType), dropdownValue);

            // return the dynamic filter
            return dynamicFilter;
        }

        /// <summary>
        /// Get a dynamic filter object from the String filter leaf node
        /// </summary>
        /// <param name="filterLeafNode"></param>
        /// <returns>Object of type T that wraps the Dynamic Filter</returns>
        public string GetStringDynamicFilter(FilterLeafNode filterLeafNode)
        {
            var dynamicFilter = "";

            var filterValue = filterLeafNode.Value.ToString();

            // if the string value is empty return
            if (string.IsNullOrWhiteSpace(filterValue))
            {
                return dynamicFilter;
            }

            var value = filterLeafNode.Value;

            var target = filterLeafNode.Target;

            var dynamicOperator = GetOperatorFromOperatorType(filterLeafNode.OperationType, InnerDataFilterType.String);

            // Go through regular string procesing if we are not working with contains
            if (filterLeafNode.OperationType != FilterOperationType.Contains)
            {
                dynamicFilter = string.Format(@"{0} {1} ""{2}""", target, dynamicOperator, value);
            }
            else
            {
                // Use special dynamic filter construction if working with contains    
                dynamicFilter = string.Format(@"{0}.Contains(""{1}"")", target, value);
            }

            return dynamicFilter;
        }

        #endregion

        #region  Filter Operation Type Processing

        /// <summary>
        /// Return a string operator for the filter comparisons based on the filter inner data type.
        /// </summary>
        /// <param name="operationType">The operation type</param>
        /// <param name="dataType"> </param>
        /// <returns></returns>
        private string GetOperatorFromOperatorType(FilterOperationType operationType, InnerDataFilterType dataType)
        {
            switch (operationType)
            {
                case FilterOperationType.GreaterThan:
                    return " > ";

                case FilterOperationType.GreaterThanOrEqual:
                    return " >= ";

                case FilterOperationType.LessThan:
                    return " < ";

                case FilterOperationType.LessThanOrEqual:
                    return " <= ";

                case FilterOperationType.Equal:
                    return " = ";

                case FilterOperationType.Contains:

                    if (dataType == InnerDataFilterType.String)
                    {
                        return " LIKE ";
                    }

                    return " = ";

                default:
                    throw new ArgumentOutOfRangeException("operationType");
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Parse the filter leaf node Value object to an Integer value. Am optional default  value can be asigned to be
        /// returned if the parsing failed.
        /// </summary>
        /// <param name="filterLeafNode">The leaf node </param>
        /// <param name="defaultValue"> </param>
        /// <returns></returns>
        private static int GetIntegerFilterValue(FilterLeafNode filterLeafNode, int defaultValue = -1)
        {
            int parsedValue = defaultValue;

            Int32.TryParse(filterLeafNode.Value.ToString(), out parsedValue);

            return parsedValue;
        }

        #endregion
    }


    /// <summary>
    /// Internally used enum for generating dynamic string filters
    /// </summary>
    internal enum InnerDataFilterType
    {
        String,
        Number,
        Date
    }
}