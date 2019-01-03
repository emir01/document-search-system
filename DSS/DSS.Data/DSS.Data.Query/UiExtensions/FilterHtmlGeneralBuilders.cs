using System.Collections.Generic;
using System.Web.Mvc;

namespace DSS.Data.Query.UiExtensions
{
    /// <summary>
    /// Defines a collection of general extensions to be used for creating
    /// data query interfaces based on the Filter Scaffolding functionality.
    /// 
    /// 
    /// This class contains the most general functionality, regarding general
    /// input html generation
    /// </summary>
    public static class FilterHtmlGeneralBuilders
    {
        #region General Filter UI elements

        /// <summary>
        /// Return a general Text Input Html Element defined by a tag builder. 
        /// The text input will have the specific css class applied to it.
        /// </summary>
        /// <param name="cssClass">The specific css classes that will be applied to the text input</param>
        /// <returns></returns>
        public static TagBuilder FilterTextInput(string cssClass)
        {
            var tagBuilder = new TagBuilder("input");

            tagBuilder.AddAttribute("type", "text");

            tagBuilder.AddCssClass(cssClass);

            return tagBuilder;
        }

        /// <summary>
        /// Construct a simple select tag that can contain multiple option tags
        /// </summary>
        /// <param name="cssClass">The general class to add to the select list</param>
        /// <returns></returns>
        public static TagBuilder FilterSelectInput(string cssClass)
        {
            var tagBuilder = new TagBuilder("select");

            tagBuilder.AddCssClass(cssClass);

            return tagBuilder;
        }

        /// <summary>
        /// Create a general filter label ui element
        /// </summary>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static TagBuilder FilterInputLabel(string cssClass)
        {
            var tagBuilder = new TagBuilder("label");

            tagBuilder.AddCssClass(cssClass);

            return tagBuilder;
        }

        /// <summary>
        /// Build an option tag with the given optional value and display properties
        /// </summary>
        /// <param name="value">The value property for the select option</param>
        /// <param name="dispaly">The display property for the select option</param>
        /// <returns>A tag builder containing a </returns>
        public static TagBuilder FilterSelectOption(string value = "", string dispaly = "")
        {
            var tagBuilder = new TagBuilder("option");

            if (!string.IsNullOrWhiteSpace(value))
            {
                tagBuilder.AddAttribute("value", value);
            }

            if (!string.IsNullOrWhiteSpace(dispaly))
            {
                tagBuilder.InnerHtml = dispaly;
            }

            return tagBuilder;
        }

        #endregion

        #region Tag Builder Extensions

        /// <summary>
        /// Add an attribute to current Tag Builder configuration directly.
        /// </summary>
        /// <param name="tagBuilder">The extending instance of the Tag Builder</param>
        /// <param name="attributeKey">The key for the attribute added to the current Tag Builder Configuration</param>
        /// <param name="attributeValue">The actual attribute value for the current key</param>
        public static void AddAttribute(this TagBuilder tagBuilder, string attributeKey, string attributeValue)
        {
            tagBuilder.Attributes.Add(new KeyValuePair<string, string>(attributeKey, attributeValue));
        }

        #endregion
    }
}
