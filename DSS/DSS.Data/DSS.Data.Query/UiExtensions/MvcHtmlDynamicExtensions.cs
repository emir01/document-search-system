using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using DSS.Data.Query.Enums;
using DSS.Data.Query.FilterScaffolding;
using DSS.Data.Query.Filters;
using DSS.Data.Query.UiExtensions.Exceptions;
using DSS.Data.Query.UiExtensions.Exceptions.PrefilterData;

namespace DSS.Data.Query.UiExtensions
{
    /// <summary>
    /// Contains Dynamic MVC HTML Filter  Extensions for generating UI elements
    /// for filter nodes defined in 
    /// </summary>
    public static class MvcHtmlDynamicExtensions
    {

        #region Public Extensions Entry Points

        /// <summary>
        /// Create an HTML element for the Filter Node defined in the FilterScaffoldModel object.
        /// 
        /// </summary>
        /// <param name="scaffoldModel"> The scaffold model containing the filter nodes </param>
        /// <param name="nodeName"> The name of the node that we will be parsing and creating a filter ui element for </param>
        /// <param name="createLabel">Flag indicating if the helper will wrap the filter input element in a parent also containing a label </param>
        /// <param name="cssClasses"> A custom css class collection for the filter input element </param>
        /// <returns>UI component referencing the string</returns>
        public static MvcHtmlString DynamicFilterElement<T>(FilterScaffoldModel<T> scaffoldModel, string nodeName, bool createLabel, string cssClasses = "")
        {
            
            var node = scaffoldModel.GetLeafNodeByName(nodeName);

            TagBuilder tagBuilder;

            // determine the type of inputs
            switch (node.FilterDataType)
            {
                case FilterDataType.String:
                    tagBuilder = FilterHtmlGeneralBuilders.FilterTextInput(cssClasses);
                    break;
                case FilterDataType.Integer:
                    tagBuilder = FilterHtmlGeneralBuilders.FilterTextInput(cssClasses);
                    break;
                case FilterDataType.Decimal:
                    tagBuilder = FilterHtmlGeneralBuilders.FilterTextInput(cssClasses);
                    break;

                case FilterDataType.Date:
                    tagBuilder = FilterHtmlGeneralBuilders.FilterTextInput(cssClasses);
                    break;

                case FilterDataType.SimpleDropdown:
                    tagBuilder = FilterHtmlGeneralBuilders.FilterSelectInput(cssClasses);

                    if (!node.IsDropdownClientPopulated)
                    {
                        PopulateServerDropdown(tagBuilder, node);
                    }

                    

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Add the generic filter input element class to the input
            tagBuilder.AddCssClass("data-query-filter-input");

            //  Populate the attributes for the filter node / ui element
            tagBuilder.AddAttribute("data-filter-data-type", node.FilterDataType.ToString());

            // set the data type on the tag builder
            tagBuilder.AddAttribute("data-dropdown-value-type", node.DropdownValueType.ToString());

            tagBuilder.AddAttribute("data-filter-case", node.CaseSensitivity.ToString());

            tagBuilder.AddAttribute("data-filter-operation-type", node.OperationType.ToString());

            tagBuilder.AddAttribute("data-filter-operator", node.Operator.ToString());

            tagBuilder.AddAttribute("data-filter-target", node.Target);

            tagBuilder.AddAttribute("data-filter-label", node.Label);

            tagBuilder.AddAttribute("id", node.Target);

            // Add the fixed filter input class
            tagBuilder.AddCssClass("query-filter-input");

            return new MvcHtmlString(tagBuilder.ToString());
        }

        #endregion

        #region Dropdown Helpers

        /// <summary>
        /// Populates a tag containing  select list with options parsed from the leaf node.
        /// </summary>
        /// <param name="selectListTagBuilder"></param>
        /// <param name="node"></param>
        private static void PopulateServerDropdown(TagBuilder selectListTagBuilder, FilterLeafNode node)
        {
            var prefilterData = GetValidatedPrefilterDropdownData(node);

            var innerHtmlBuilder = new StringBuilder();

            foreach (var dataObject in prefilterData)
            {
                // Get the data object type so we can read the important properties and create the option for the select tag
                var objectType = dataObject.GetType();

                // Get and validate the display property
                var displayProperty = objectType.GetProperty(node.PreFilterDataDispaly);

                if (displayProperty == null)
                {
                    throw new PrefilterDisplayPropertyNotFoundException("Dropdown Extensions : The prefilter data object/s does not contain the specified display property");
                }

                // Get and validate the value property
                var valuePropety = objectType.GetProperty(node.PreFilterDataValue);

                if (valuePropety == null)
                {
                    throw new PrefilterValuePropertyNotFoundException("Dropdown Extensions : The prefilter data object/s does not contain the specified value property");
                }

                // Get the actual property values from the data object for both the value and display properties.
                // Get the string representatinos so we can add/display them on the UI
                var dataObjectValue = valuePropety.GetValue(dataObject).ToString();
                var dataObjectDisplay = displayProperty.GetValue(dataObject).ToString();

                // Create a option tag from the two data object properties
                var optionTagBuilder = FilterHtmlGeneralBuilders.FilterSelectOption(dataObjectValue, dataObjectDisplay);

                innerHtmlBuilder.Append(optionTagBuilder);
            }

            // Once we go through all the objects in the prefilter data for the dropdown
            // we are going to set the selectTagBuilder inner html
            selectListTagBuilder.InnerHtml = innerHtmlBuilder.ToString();
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates the prefilter data on the node as expected for the dropdown UI element
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static IEnumerable<object> GetValidatedPrefilterDropdownData(FilterLeafNode node)
        {
            // Validate the leaf node propeties related to populating dropdowns
            if (string.IsNullOrWhiteSpace(node.PreFilterDataDispaly))
            {
                throw new PrefilterDisplayPropertyNotFoundException(
                    "The Prefilter display name must be set when rendering server side dropdowns");
            }

            if (string.IsNullOrWhiteSpace(node.PreFilterDataValue))
            {
                throw new PrefilterDisplayPropertyNotFoundException("The Prefilter display nam");
            }

            var prefilterData = node.PreFilterData as IEnumerable<object>;

            // if the data element is not an Enumerable collection of objects
            if (prefilterData == null)
            {
                throw new NonEnumerablePrefilterException(
                    "The prefilter data object must be an enumerable collection of object if the data type is set to dropdown");
            }

            return prefilterData;
        }

        #endregion
    }
}
