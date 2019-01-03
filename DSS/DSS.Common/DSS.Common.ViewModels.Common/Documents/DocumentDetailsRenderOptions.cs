namespace DSS.Common.ViewModels.Documents
{
    /// <summary>
    /// View model containing boolean flags regarding the document details 
    /// rendering functionality
    /// </summary>
    public class DocumentDetailsRenderOptions
    {

        #region Constructor

        public DocumentDetailsRenderOptions()
        {
            DocumentId = "";
            RenderDownload = true;
            RenderStats = true;
            RenderVoting = true;
        }

        #endregion

        #region Document Flags

        /// <summary>
        /// The id of the document. Present on the render options view model for ease of access and model properties grouping
        /// </summary>
        public string DocumentId { get; set; }

        #endregion

        #region Rendering Flags

        /// <summary>
        /// Should the partial view render the voting controls
        /// </summary>
        public bool RenderVoting { get; set; }

        /// <summary>
        /// Should the detail partial view render the download control
        /// </summary>
        public bool RenderDownload { get; set; }

        /// <summary>
        /// Should the partial view render the general documents stats
        /// </summary>
        public bool RenderStats { get; set; }

        #endregion
    }
}
