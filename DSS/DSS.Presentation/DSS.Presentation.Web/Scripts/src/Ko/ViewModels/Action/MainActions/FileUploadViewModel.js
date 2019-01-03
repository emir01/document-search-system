// Global view model handling the process of uploading files.
(function () {
    var fileUploadViewModel = (function () {

        //=============================================================================
        //      Properties
        //=============================================================================

        // Short references to view models
        var doc = DSS.Ko.ViewModels.Document.DocumentViewModel;

        //=============================================================================
        //      Observables
        //=============================================================================

        // Observables
        var documents = ko.observableArray([]);

        // Private properties
        var keywordRepository;
        var categoryRepository;

        // The custom callback object to stop the loadmask
        var stopLoadmaskObject;

        // categories
        var availableCategories = ko.observableArray([]);
        
        // tags/keywords
        var availableKeywords = ko.observableArray([]);

        //=============================================================================
        //      Public Functions
        //=============================================================================

        // Initialize the file upload view model.
        var init = function () {
            keywordRepository = new DSS.Common.Framework.BaseRepository("keyword", "api");
            
            categoryRepository = new DSS.Common.Framework.BaseRepository("category", "api");

            $("#fixedFileUploadActions").sticky({topSpacing:60});

            // start the loadmask to retrieve the keywords
            // and the categories
            stopLoadmaskObject = new DSS.Utilities.AjaxCallback({
                numRequest: 2,
                singleCallback: function () {
                    DSS.load.ClearHtml();
                }
            });

            DSS.load.MaskHtml();

            keywordRepository.GetAll(successKeywordsGet, function () {
                alert("Failed to retrieve keywords");
            });

            categoryRepository.GetAll(successCategoriesGet, function () {
                alert("Failed to retrieve categories");
            });
        };
        
        //=============================================================================
        //      Event Handlers
        //=============================================================================

        // Add a new document to the document upload collection
        var addDocument = function () {
            var newDoc = new doc("", "");
            documents.push(newDoc);
        };

        // Remove a document from the document upload list. Event handler for the individual 
        // document template remove button
        var removeDocument = function(e) {
            documents.remove(this);
        };
        
        // This makes sure all the file data is in order and displays 
        // a mesasge if something is wrong. Currently directly submites the form to the server
        var checkFileIntegrityAndUpload = function (data, event) {
            // Go trhough the documents observable
            // remvoe unvalid stuff from form data. Fow now stay as it is
            
            // get the file upload form
            var formSubmitButton = $("#hidden-file-upload-submit");

            console.log("Found Forms: " + formSubmitButton.size());
            
            // submit the form  for now
            formSubmitButton.click();
        };

        // event handler for selecting a document using any of the document selectors
        // This is bound to the change event on the file upload input controll.
        // When its value changes we are going to perform some precalculatins so we can tell files
        // apart on the server
        var newDocumentSetInUpload = function (data, event) {
            var fileUpload = event.target;
            var $fileUpload = $(fileUpload);

            // get the name of the file.
            var name = $fileUpload.val();

            // set the name to the value of the hidden document key field.
            var sibling = $fileUpload.parents(".file-upload-control-wrapper").find(".document-key");

            sibling.val(name);
            data.title(name);
        };

        //=============================================================================
        //      Knockout Event Handlers
        //=============================================================================
        
        // After the template is rendered we will be setting apropriate 
        // ids to reach the file controller.
        var handleAfterRender = function (elements, data) {
            var fileUploads = $(elements).find(".fancy-files");

            fileUploads.customFileInput();
            // Set the ids of the input elements    
            var documentNumber = documents().length;

            $(elements).find(".document-title").attr("id", "documents[" + (documentNumber - 1) + "]_Title")
                                                   .attr("name", "documents[" + (documentNumber - 1) + "].Title");

            $(elements).find(".document-author").attr("id", "documents[" + (documentNumber - 1) + "]_AuthorName")
                                                    .attr("name", "documents[" + (documentNumber - 1) + "].AuthorName");

            $(elements).find(".document-categories").attr("id", "documents[" + (documentNumber - 1) + "]_CategoryList")
                                                 .attr("name", "documents[" + (documentNumber - 1) + "].CategoryList");

            $(elements).find(".document-keywords").attr("id", "documents[" + (documentNumber - 1) + "]_KeywordsList")
                                                 .attr("name", "documents[" + (documentNumber - 1) + "].KeywordsList[]");

            $(elements).find(".document-key").attr("id", "documents[" + (documentNumber - 1) + "]_DocumentKey")
                                                 .attr("name", "documents[" + (documentNumber - 1) + "].DocumentKey");

            // Set the id for the file upload control
            fileUploads.attr("id", "document" + documentNumber);
        };
        
        //=============================================================================
        //      Request Callbacks
        //=============================================================================
        
        // The erequest callback for the keywords
        var successKeywordsGet = function (data, status, object) {
            availableKeywords(data);
            stopLoadmaskObject.requestComplete(true);
        };
        
        // The server request callback for the categories
        var successCategoriesGet = function (data, status, object) {
            availableCategories(data);
            stopLoadmaskObject.requestComplete(true);
        };

        return {
            init: init,
            Documents: documents,
            
            AvailableKeywords: availableKeywords,
            AvailableCategories: availableCategories,
            
            AddDocument: addDocument,
            RemoveDocument: removeDocument,
            
            handleAfterRender: handleAfterRender,
            
            checkFileIntegrityAndUpload: checkFileIntegrityAndUpload,
            
            newDocumentSetInUpload: newDocumentSetInUpload
        };
    })();

    // namespace the view model.
    DSS.namespace("Ko.ViewModels.Action.FileUploadViewModel", fileUploadViewModel);

})(jQuery);