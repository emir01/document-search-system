using System;
using System.ComponentModel.DataAnnotations;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// Database entitiy containing information on User Document downloads.
    /// </summary>
    public class DownloadLog:BaseEntitiy
    {
        #region Properties

        /// <summary>
        /// When did the user download the document
        /// </summary>
        public DateTime DownloadDate { get; set; }

        #endregion

        #region Navigation Variables

        /// <summary>
        /// Which user initiated the download
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// What document was downloaded
        /// </summary>
        [Required]
        public Document Document { get; set; }
        
        #endregion
    }
}
