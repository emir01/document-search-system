// Global view model for the document search and filter page 
(function ($) {
    var documentSearchViewModel = (function () {

        // Utilities objects
        var categoryRepository;

        // Short reference to the Search Query Module 
        var searchQueryModule = DSS.Ko.Utilities.SearchQueryModule;

        // the custom callback object used to manage the initial loading
        // screen
        var stopLoadmaskObject;

        /*
            Observables
            ================================================================================================================
            ================================================================================================================
        */

        // the documents
        var documents = ko.observableArray([]);

        // the observable notifying the view that there are no matching elements
        var noMatchingDocuments = ko.observable(false);

        // Search related string observables
        var titleSearch = ko.observable("");

        var contentSearch = ko.observable("");

        var authorSearch = ko.observable("");

        // all the categories to be used as filtering options
        var categories = ko.observableArray([]);
        var selectedCategories = ko.observableArray([]);

        /*
            Initialization
            ================================================================================================================
            ================================================================================================================
        */

        // The base initialization function and helpers
        var init = function () {
            setupPNotifiy();

            // create a custom callback object to handle the loading screen
            stopLoadmaskObject = new DSS.Utilities.AjaxCallback({
                numRequest: 2,

                singleCallback: function () {
                    DSS.load.ClearHtml();
                }
            });

            // start the initial loading screen
            DSS.load.MaskHtml();

            // Initialize the SerchQueryModule
            searchQueryModule.Init(documents);

            searchQueryModule.doQuery(documents, doQueryCallback);

            // Create the category repository and get all the current categories
            categoryRepository = new DSS.Common.Framework.BaseRepository("category", "api");
            categoryRepository.GetAll(successInitialCategories, function () {
                DSS.msg.error("Categories", 'Failed to retrieve categories.');
            });
    
            selectedCategories.subscribe(selectedCategoriesChanged);
        };

        // setup the custom notify timer for the page
        var setupPNotifiy = function () {
            // Setup the delay on the messages
            $.pnotify.defaults.delay = 5000;
        };

        /*
            Data Callbacks
            ================================================================================================================
            ================================================================================================================
        */

        /*
            The callback for the do query functionality from the Search Query Module.
            
            This handles the doQuery on every call including filtering/serach calls.

            Currently only implemented to work with the functionality that runs a search from beginning, 
            that is no fetch next processing functionality.
        */

        var doQueryCallback = function (data, customStatus, object) {
            stopLoadmaskObject.requestComplete(true);

            if (customStatus) {
                DSS.msg.success("Documents", "Retrieved " + data.length + " documents.");
            }
            else {
                DSS.msg.error("Documents", 'Failed to retrieve documents.');
            }

            // if there are no documents set the proper observable
            if (data = null || data.length === 0) {
                noMatchingDocuments(true);
            } else {
                noMatchingDocuments(false);
            }
        };

        // data callback for the initial category retrieval
        var successInitialCategories = function (data, status, object) {
            stopLoadmaskObject.requestComplete(true);

            DSS.msg.success("Categories", "Retrieved " + data.length + " categories.");

            categories(data);
        };

        /*
            Utilities
            ================================================================================================================
            ================================================================================================================
        */

        var selectedCategoriesChanged = function () {
            syncCategorySelectBoxWithUrlParameters();
        };

        /*
            Clear the observables used to track and store search parameters
        */

        var clearSearchObservables = function () {
            authorSearch("");

            titleSearch("");

            selectedCategories([]);

            contentSearch("");
        };

        /*
            Make any preparations and run the query after applying internal criteria. 
            This is used to trigger the Search Query Module to do a query after an event has fired
            that changes the composition of the criterias.
        */

        var runSearch = function () {
            // Display a loadmask and run the query
            DSS.load.MaskHtml();

            searchQueryModule.doQuery(documents, doQueryCallback);
        };


        /*
         * This should get called each time category filtering changes independantly from the
         * source of change
         */

        var syncCategorySelectBoxWithUrlParameters = function () {
            
        };

        /*
            Event Handlers
            ================================================================================================================
            ================================================================================================================
        */

        /*
            The event handler for the download button on the 
            idividual document results on the search page
        */
        var downloadDocument = function (data, event, element) {
            var linkHref = $(event.target).attr('href');

            // use a file download plugin
            $.fileDownload(linkHref);

            return false; //this is critical to stop the click event which will trigger a normal file download!
        };

        /*
            Event handler for clicking a category displayed on each of the individual document
            results
        */

        var documentCategoryClick = function (data, event) {
            // call the query modules add category to query function
            searchQueryModule.extendQueryForCategory(documents, data);

            selectedCategories.push(data);

            runSearch();

            // because of anchors
            return false;
        };

        /*
            Event handler for clicking a keyword displayed on each of the individual document
            results
        */

        var documentKeywordClick = function (data, event) {
            // call the query modules add category to query function
            searchQueryModule.extendQueryForKeyword(documents, data);

            runSearch();

            // because of anchors
            return false;
        };

        /*
            The event handler for the Search Button on the criteria dashboard. Handles and updates the Query Module 
            regarding the content, title, author and specific selected category criterias
        */

        var applyCriteria = function () {
            var title = titleSearch().trim();

            var content = contentSearch().trim();

            var author = authorSearch().trim();

            // Extend or Remove Title from Query
            if (title != "") {
                searchQueryModule.extendQueryForTitle(documents, title);
            }
            else {
                searchQueryModule.removeTitleFromQuery(documents);
            }

            // Extend or Remove Author from Query
            if (author != "") {
                searchQueryModule.extendQueryForAuthor(documents, author);
            }
            else {
                searchQueryModule.removeAuthorFromQuery(documents);
            }

            // Extend or Remove Content from Query
            if (content != "") {
                searchQueryModule.extendQueryForContent(documents, content);
            }
            else {
                searchQueryModule.removeContentFromQuery(documents);
            }

            var selectedCats = selectedCategories();

            console.log("Current selected categories: " + JSON.stringify(selectedCats));
            
            // on apply criteria we are always going to clear all categories and re-apply only
            // selected categories in the hash
            searchQueryModule.removeAllCategoriesFromQuery();

            // re add all the categories that are selected form the observable array.
            $.each(selectedCats, function (index, value) {
                // add the value to the query params
                console.log("Extending query for category: " + value);
                searchQueryModule.extendQueryForCategory(documents, value);
            });

            // Run the search after everything has been set up.
            // Will prevent multiple sever trips on each observable update
            runSearch();
        };

        /*
            Clear all applied criteria to the search parameters
        */

        var clearCriteria = function () {
            // notify the Url Search utility to clear all has parameters
            searchQueryModule.clearAllCriteria(documents, doQueryCallback);

            clearSearchObservables();

            runSearch();
        };

        /*
            RMP
            ================================================================================================================
            ================================================================================================================
        */

        // The object defining the view model
        return {
            init: init,

            // The main documents observable containing the documents
            documents: documents,

            // The search related observables
            titleSearch: titleSearch,
            contentSearch: contentSearch,
            authorSearch: authorSearch,

            // Observables
            categories: categories,
            selectedCategories: selectedCategories,

            // flag indicating that there are no matching documents
            noMatchingDocuments: noMatchingDocuments,

            // Events
            downloadDocument: downloadDocument,

            // The Search related event handlers
            documentCategoryClick: documentCategoryClick,
            documentKeywordClick: documentKeywordClick,

            applyCriteria: applyCriteria,
            clearCriteria: clearCriteria
        };
    })();
    
    // Namespace the view model in the DSS global namespace
    DSS.namespace("Ko.ViewModels.Action.DocumentSearchViewModel", documentSearchViewModel);
})(jQuery);
