using System;

namespace DSS.Common.ViewModels.Categories
{
    /// <summary>
    ///  A simple view model for displaying/selecting categories.
    /// </summary>
    public class DisplayCategoryViewModel
    {
        /// <summary>
        /// The id of the Category. If empty a new catecory can be created.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The alias of the category.
        /// </summary>
        public string Alias { get; set; }
    }
}
