using System;

namespace DSS.Common.ViewModels.Keywords
{
    /// <summary>
    /// Simple View model to display/select keyword entities.
    /// </summary>
    public class DisplayKeywordViewModel
    {
        /// <summary>
        /// The id of the entitiy. Could be entitiy under which case a new Keyword will be created
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        ///  The name of the keyword
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The alias for the keyword
        /// </summary>
        public string Alias { get; set; }
    }
}
