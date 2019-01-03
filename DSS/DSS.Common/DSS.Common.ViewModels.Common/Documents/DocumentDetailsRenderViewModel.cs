namespace DSS.Common.ViewModels.Documents
{
    /// <summary>
    /// Document Details Render View Model used to render the document entitiy detail view.
    /// 
    /// The view model is to be manually constructed on the Controller based on BL results
    /// </summary>
    public class DocumentDetailsRenderViewModel
    {
        #region Properties
        
        /// <summary>
        /// Internally contains the simple document display view model
        /// </summary>
        public DisplayDocumentViewModel DisplayDocumentViewModel { get; set; }

        /// <summary>
        /// Contains document stat information
        /// </summary>
        public DocumentStatsViewModel DocumentStatsViewModel { get; set; }

        /// <summary>
        /// Rendering options
        /// </summary>
        public DocumentDetailsRenderOptions Options { get; set; }

        /// <summary>
        /// Flag, basically indicating the succesfull retrieval of a domain entity that will be rendered
        /// </summary>
        public bool EntityRetrieved { get; set; }
        
        /// <summary>
        /// Rendering message notfying the Client if anything goes wrong in the entity retrieval/rendering process
        /// </summary>
        public string RenderMessage { get; set; }

        #endregion

        #region Constructon

        /// <summary>
        /// Get a renderer object for a failed entitiy  retrieval from the Domain Services
        /// </summary>
        /// <returns><see cref="DocumentDetailsRenderViewModel"/> object with a failed EntityRetrieved property</returns>
        public static DocumentDetailsRenderViewModel  GetFailedRenderer(string message= "")
        {
            var renderer = new DocumentDetailsRenderViewModel
                               {EntityRetrieved = false, DisplayDocumentViewModel = new DisplayDocumentViewModel(),RenderMessage = message};

            return renderer;
        }

        /// <summary>
        /// Get a propper renderer object for a succesful document retrieval
        /// </summary>
        /// <returns></returns>
        public static DocumentDetailsRenderViewModel GetSuccessRenderer(string message="")
        {
            var renderer = new DocumentDetailsRenderViewModel {EntityRetrieved = true, RenderMessage = message};

            // set the default rendering options for the success renderer

            renderer.Options = new DocumentDetailsRenderOptions();
            
            return renderer;
        }

        #endregion
    }
}
