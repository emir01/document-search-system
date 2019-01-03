using System.Collections.Generic;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// The basic entity class representing document keywords.
    /// </summary>
    public class Keyword:BaseEntitiy
    {
        #region  Simple Properties

        /// <summary>
        /// The name of the keyword.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The short alias for the keyword.
        /// </summary>
        public string Alias { get; set; }

        #endregion
    }
}
