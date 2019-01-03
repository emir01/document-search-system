using System.Collections.Generic;
using System.Web;
using DSS.Common.ViewModels.Documents;

namespace DSS.Bootstrap.Utilities.Dtos
{
    /// <summary>
    /// Utility class used to combine the results of the file uploads processing.
    /// reerere
    /// </summary>
    public class FileUploadProcessingResults
    {
        #region Constructor

        public FileUploadProcessingResults()
        {
            SavedFiles = new List<AddDocumentViewModel>();
            OrphanViewModels = new List<AddDocumentViewModel>();
            OrphanUploadedFiles = new List<HttpPostedFileBase>();
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// A boolean value used as a general indicator of the success of the operation.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// The general message about the success of the operation
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Contains all the Add Document View models with saved files
        /// </summary>
        public List<AddDocumentViewModel> SavedFiles { get; set; }

        /// <summary>
        /// Contains all the view models that have no corresponding files.
        /// </summary>
        public List<AddDocumentViewModel> OrphanViewModels { get; set; }

        /// <summary>
        /// Contains all the files that dont have corresponding view models.
        /// </summary>
        public List<HttpPostedFileBase> OrphanUploadedFiles { get; set; }

       #endregion
    }
}