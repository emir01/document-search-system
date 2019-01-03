// The action view model handling any functionality for the document  details screen rendered by server
// side partials
(function ($) {

    /*
        The view model and initialization code that will handle any initialization for document details rendered views
    */

    var documentDetailsViewModel = (function () {

        /*
           Properties
           ==================================================================
       */

        /*
           Observables
           ==================================================================
        */

        /*
            Initialization
            ==================================================================
        */

        /*
            The main initialization  function
        */
        
        var init = function () {

            setupEvents();
        };

        /*
            Initialize event handlers for the document actions
        */

        var setupEvents = function () {

            $("body").on("click", ".document-upvote-button", upvoteButtonEventHandler);
            $("body").on("click", ".document-downvote-button", downvoteButtonEventHandler);
            
            $("body").on("click", "a.download-file-link", downloadDocumentEventHandler);

        };

        /*
             Event Handlers
            ==================================================================
        */
        
        /*
            Event handler that will process document download requests
        */

        var downloadDocumentEventHandler = function(data, event) {
            var linkHref = $(this).attr('href');
            
            $.fileDownload(linkHref);
            
            return false; //this is critical to stop the click event which will trigger a normal file download!
        };

        /*
            Event handler for the upvote button on the document details view.
        */

        var upvoteButtonEventHandler = function (data, event) {
            var documentId = $(this).attr("data-document-id");

            DSS.msg.info("Document Details", "Upvote!! " + documentId);
        };

        /*
            Event handler for the downvote button on the document details view
        */

        var downvoteButtonEventHandler = function (data, event) {
            var documentId = $(this).attr("data-document-id");

            DSS.msg.info("Document Details", "Downvote!! " + documentId);
        };

        /*
             Callbacks
            ==================================================================
        */

        // The object defining the view model
        return {
            init: init,
        };
    })();

    // Namespace the view model in the DSS global namespace
    DSS.namespace("Ko.ViewModels.Action.DocumentDetailsViewModel", documentDetailsViewModel);
})(jQuery);
