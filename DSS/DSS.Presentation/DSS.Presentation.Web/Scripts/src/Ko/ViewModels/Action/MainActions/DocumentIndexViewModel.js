(function ($) {
    // The document indexing view model 
    var documentIndexViewModel = (function () {

        // ==== Observables

        // Specify the dashboard observable values

        var documentsInIndex = ko.observable(120);
        var searchableDocuments = ko.observable(120);
        var totalUploadedDocuments = ko.observable(300);
        var isIndexOptimized = ko.observable("N/S");

        // ==== Initialization functions

        /*
            Basic Properties and Data
        */

        var dataTable;

        /*
            Initializations
            ======================================================================================
            ======================================================================================
        */

        // the main initialization function
        var init = function () {
            setupDataTablesAuto();

            setupBootsrapUiElements();

            setupDashboardStats();
        };

        /*
            Setup the dashboard stats functionality
        */

        var setupDashboardStats = function () {
            refreshDashboardStats();
        };

        /*
            Initialize bootstrap elements
        */

        var setupBootsrapUiElements = function () {
            $(".dashboard-widget").tooltip();
        };

        /*
            Construct the document table using the client side data tables
            build extensions
        */

        var setupDataTablesAuto = function () {
            var dataTableConfig = DSS.query.tables.build.StartTableConfig();

            // Set the selector before calling execute 
            dataTable = dataTableConfig.setSelector("#DocumentsTable")
                .enableServerProcessing(DSS.url.GetUrl("ProcessDataTableRequest", "DocumentsDashboard"))

                .setColumn(0, "Title")
                .setColumn(1, "AuthorName", 120)
                .setColumn(2, "IsIndexed_Descriptive", 70)

                .setActionColumn(3, "Id", 140)
                .addAction(decideIndexingActionText, false, "index-document", decideIndexActionIcon, addToIndexButtonHandler)
                .addAction("Details", false, "details-document", "icon-eye-open", detailsButtonHandler)
                .addAction("Download Document", false, "download-document", "icon-download-alt", downloadEventHandler)
            
                // add the filtering functionality using the filters from the document dashboard filter
                .filtering("#document-dashboard-filter")
            
                //setup paging
                .disableRowCountSelection()
                .setDefaultRowCount(5)
            
                .setGridCallback(DSS.query.tables.objects.CallbackNames.FnDrawCallback, fnDrawCallback)
                .setGridCallback(DSS.query.tables.objects.CallbackNames.FnPreDrawCallback, fnPreDrawCallback)
            
                .execute();
        };

        /*
            Helper function callback for deciding the Text to use for the action 
            used to index documents on the grid. Function is used by the fluent Data Tables
            configurator as a callback
        */
        
        var decideIndexingActionText = function(boundValue, type, dataContext) {
            var indexButtonTitle = dataContext.IsIndexed == true ? "Remove from index" : "Add to index";
            return indexButtonTitle;
        };
        
        /*
            Helper functin callback for deciding the icon class to usee for the grid action
            used to index documents in the grid
        */
        
        var decideIndexActionIcon = function (boundValue, type, dataContext) {
            var indexButtonClass = dataContext.IsIndexed == true ? "icon-minus" : "icon-plus";
            return indexButtonClass;
        };

        /*
            The draw callback for the Documents grid used to close up
        */
        
        var fnDrawCallback = function (oSettings) {
            DSS.load.ClearSpecific(dataTable);
        };
        
        /*
            The pre draw callback for the Documents grid
        */

        var fnPreDrawCallback = function (oSettings) {
            // Mask the table only if it is defined at this callback
            if(dataTable != undefined) {
                DSS.load.MaskSpecific(dataTable, "Loading Data...");
            }
        };

        /*
            Event Handlers
            ======================================================================================
            ======================================================================================
        */

        /*
            The event handler for the apply documents filter button
        */

        var applyDocumentsFilter = function (data, event) {

            // call the draw event on the table which will  execute the filtering
            dataTable.fnDraw();
        };
        
        /*
            The event handler for the clear documents filter button
        */
        
        var clearDocumentsFilter = function (data, event) {
            // Clear all the filter values and redraw the Grid
            DSS.query.tables.filter.ClearFilterValues("#document-dashboard-filter");
            dataTable.fnDraw();
        };

        /*
            Event handler for the add to index button action on the document grid
        */

        var addToIndexButtonHandler = function (event) {
            var button = $(this);
            var docId = getDocumentIdForButton(button);

            var isIndexed = getDocumentIndex(button);

            var data = { id: docId };

            var url;
            var postObject;

            if (isIndexed) {
                url = DSS.url.GetUrl("RemoveFromIndex", "DocumentsDashboard");
                postObject = DSS.Utilities.Ajax.GetAjaxPostObject(url, data, removeFromIndexSuccess, removeFromIndexError);
            }
            else {
                url = DSS.url.GetUrl("AddToIndex", "DocumentsDashboard");
                postObject = DSS.Utilities.Ajax.GetAjaxPostObject(url, data, addToIndexSuccess, addToIndexError);
            }

            DSS.load.MaskSpecific(dataTable, "Modifying Index Status...");
            $.ajax(postObject);
        };

        /*
            Event handler for the grid details button
        */

        var detailsButtonHandler = function () {
            var button = $(this);
            var docId = getDocumentIdForButton(button);

            DSS.msg.success("Document Dashboard", "Document Details Handler for " + docId);
        };

        /*
            Event handler for the download action button
        */
        var downloadEventHandler = function () {
            var button = $(this);
            var docId = getDocumentIdForButton(button);

            DSS.msg.success("Document Dashboard", "Download Document Handler for " + docId);
        };

        /*
            Event handler for the refresh  dashboard stats button.
        */

        var dashboardRefreshButtonHandler = function () {
            refreshDashboardStats();
        };

        /*
            Utilities
            ======================================================================================
            ======================================================================================
        */

        /*
            Refresh the dashboard functionality by making the ajax call and refreshing the 
            observable stats. This is an inner call usually called on initialization or via events
        */

        var refreshDashboardStats = function () {
            var url = DSS.url.GetUrl("UpdateDashboardStats", "DocumentsDashboard");

            var ajaxObject = DSS.Utilities.Ajax.GetAjaxGet(url, null, updateDashboardSuccess, updateDashboardFailed);
            maskDashboardElements();

            $.ajax(ajaxObject);
        };

        /*
            Used to add masks to all dashboard elements
        */

        var maskDashboardElements = function () {
            DSS.load.MaskSpecific($(".dashboard-widget"), "Loading Stat...");
        };

        /*
            Used to clear the masks from all the dashboard elements
        */

        var unmaskDashboardElements = function () {
            DSS.load.ClearSpecific($(".dashboard-widget"));
        };

        /*  
            Get the document id for the row with the activated action button
        */

        var getDocumentIdForButton = function (actionButton) {
            var row = actionButton.parents("tr")[0];
            var data = dataTable.fnGetData(row);
            return data.Id;
        };

        /*
            Return the indexes status for an element
        */

        var getDocumentIndex = function (actionButton) {
            var row = actionButton.parents("tr")[0];
            var data = dataTable.fnGetData(row);
            return data.IsIndexed;
        };

        /*
            Server Callbacks
            ======================================================================================
            ======================================================================================
        */

        /*
            Callback from the add to index action
        */

        var addToIndexSuccess = function (response, status, object) {

            DSS.load.ClearSpecific(dataTable);

            if (response.Status) {
                DSS.msg.success("Documents Dashboard", "Success in adding document to index");
                dataTable.fnDraw(true);
            }
            else {
                DSS.msg.error("Documents Dashboard", "Error in adding document to index.Please try again later");
            }
        };

        /*
            Callback for the add to index action
        */

        var addToIndexError = function (object, status, thrown) {
            DSS.load.ClearSpecific(dataTable);
            DSS.msg.error("Documents Dashboard", "Error in adding document to index request. Please try again later.");
        };


        /*
            Callback for the removal from the index success request
        */

        var removeFromIndexSuccess = function (response, status, object) {

            DSS.load.ClearSpecific(dataTable);

            if (response.Status) {
                DSS.msg.success("Documents Dashboard", "Success in removing document from index");
                dataTable.fnDraw(true);
            }
            else {
                DSS.msg.error("IndexDocuments Dashboard", "Error in removing document from index.Please try again later");
            }
        };

        /*
            Callback for the removal from the index error request
        */

        var removeFromIndexError = function (object, status, thrown) {
            DSS.load.ClearSpecific(dataTable);
            DSS.msg.error("IndexDocuments Dashboard", "Error in removing document from index request. Please try again later.");
        };

        /*
            Update dashboard success callback
        */
        var updateDashboardSuccess = function (response, status, object) {
            unmaskDashboardElements();

            if (response.Status) {
                var data = response.Data;

                documentsInIndex(data.DocumentsInIndex);
                searchableDocuments(data.SearchableDocuments);
                totalUploadedDocuments(data.TotalUploadedDocuments);
                isIndexOptimized(data.IsIndexedOptimized);

            }
            else {
                DSS.msg.error("Documents Dashboard", "Error retrieving some of the dashboard information.");
            }
        };

        /*
            Update dashboard error callback
        */

        var updateDashboardFailed = function (object, status, thrown) {
            unmaskDashboardElements();
            DSS.msg.error("Documents Dashboard", "Error retrieving some of the dashboard information.");
        };

        // ==== The actual view model object returned to the UI`
        return {
            Initialize: init,

            // dashboard status observables

            DocumentsInIndex: documentsInIndex,
            SearchableDocuments: searchableDocuments,
            TotalUploadedDocuments: totalUploadedDocuments,
            IsIndexOptimized: isIndexOptimized,

            // Publioc Event handlers
            DashboardRefreshButtonHandler: dashboardRefreshButtonHandler,
            
            // Document Filtering Handlers
            ApplyDocumentsFilter: applyDocumentsFilter,
            ClearDocumentsFilter : clearDocumentsFilter
        };
    })();

    // Namespace the view model in the DSS global namespace
    DSS.namespace("Ko.ViewModels.Action.DocumentIndexViewModel", documentIndexViewModel);
})(jQuery);
