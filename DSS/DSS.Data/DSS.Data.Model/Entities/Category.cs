using System.Collections.Generic;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// The basic entity class used to represent document categories.
    /// </summary>
    public class Category : BaseEntitiy
    {
        #region  Simple Properties

        /// <summary>
        /// The name of the category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short alias for the category.
        /// </summary>
        public string Alias { get; set; }

        #endregion
    }
}
