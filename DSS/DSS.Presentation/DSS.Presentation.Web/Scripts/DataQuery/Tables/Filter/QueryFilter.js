/*
    Contains Data Query Filtering functionality. Enhance jquery data tables with the filter functionality
    implemented via the DataQuery on the server side
*/


(function ($) {

    /*
        jQuery Data Table extensions for working with certain aspects of the grid module.
    */

    var dataQueryFilterExtensions = (function () {

        /*
          Shorthand references
          --------------------------------------------------------------------------------------------------------------
          --------------------------------------------------------------------------------------------------------------
        */

        var objects = DSS.query.tables.filterObjects;


        /*
            Filtering extensions properites and attributes
            --------------------------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------------------------
        */

        // The name of the attribute on which the parent selector for the filter inputs
        // is stored on the grid
        var parentElementSelectorAttr = "filter-inputs-parent-selector";
        

        /*
            Filtering utility methods
            --------------------------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------------------------
        */
        
        /*
            Clear the values for all the filter input elements under the parent selector.

            Params: 
                    - filterElementsParentSelector - The parent element SELECTOR containing all the filter element inputs
                                                        built using the server side data query functionality
        */

        var clearFilterValues = function (filterElementsParentSelector) {

            // Get the filter input elements
            var $filtersParent = $(filterElementsParentSelector);
            var allFilters = $filtersParent.find(".data-query-filter-input");
            
            for(var i = 0, filtersCount = allFilters.size(); i< filtersCount; i++) {
                var $inputElement = $(allFilters[i]);
                
                // For now just set the value to an empty string
                $inputElement.val("");
            }
        };
        

        /*
            Public hook in methods to be used by client code to setup filtering for jquery Data Table grids
            --------------------------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------------------------
        */

        /*
            Link a filter element collection specified via a jquery filters parent element selector
            to the jquery data tables grid specified via the table configuration object build using the DatatableExtensions

            Params :  
                        - filterElementsParentSelector - The parent element SELECTOR containing all the filter element inputs
                                                    built using the server side data query functionality

                        - gridConfigurationObject - The grid configuration object that will be enhances to include the filtering
                                                        functionality
        */

        var linkFiltersToTable = function (filterElementsParentSelector, gridConfigurationObject) {

            /* 
                This comes down to configuring fn server params for the options in the 
                gridConfigurationObject.

                The filters are to be passed as an array in the filter scaffold model ? 
            */

            // Configure the filter flags on the configuration object
            gridConfigurationObject.isFilteringSet = true;

            gridConfigurationObject.filterParentElementSelector = filterElementsParentSelector;

            // Set the fnServerParams function
            gridConfigurationObject.options.fnServerParams = serverParamsGridCallback;
        };

        /*  
            The fnServerParams handler from the Query Filter extenions functionality
        */

        var serverParamsGridCallback = function (aoData) {
            var grid = this;
            var filterInputParentElementSelector = grid.attr(parentElementSelectorAttr);

            // Get the filter input elements
            var $filtersParent = $(filterInputParentElementSelector);
            var allFilters = $filtersParent.find(".data-query-filter-input");

            var numFilters = allFilters.size();

            var serverFilters = [];

            for (var kk = 0 ; kk < numFilters; kk++) {
                var clientFilterInput = allFilters.get(kk);
                var serverFilterObject = getServerFilterFromInput(clientFilterInput);
                serverFilters.push(serverFilterObject);
            }

            // Create the server action parameter and asign the array of server filter objects
            // actually representing filter nodes
            var filtersObject = {};
            filtersObject.FilterNodes = serverFilters;

            // Push the filter object ( Filter Scaffold Object) to the aoData for the DataTables 
            // server param collector with the expected name for the Handling Controller

            aoData.push({ name: "filters", value: filtersObject });
        };

        /*
            Private filter data construction methods
            --------------------------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------------------------
        */

        /*
            Build a server filter leaf node object from the filter input
            that has been constructed using server side helpers
        */

        var getServerFilterFromInput = function (input) {
            var jInput = $(input);

            // Extract the values from the input element

            var target = jInput.attr(objects.FilterAttributeNames.Target);

            var booleanOperator = objects.BooleanOperator[jInput.attr(objects.FilterAttributeNames.BooleanOperator)];

            var operationType = objects.FilterOperationType[jInput.attr(objects.FilterAttributeNames.OperationType)];

            var caseSensitivity = objects.FilterCaseSensitivity[jInput.attr(objects.FilterAttributeNames.CaseSensitivity)];

            var filterDataType = objects.FilterDataType[jInput.attr(objects.FilterAttributeNames.FilterDataType)];

            var dropdownValueType = objects.DropdownValueTypes[jInput.attr(objects.FilterAttributeNames.DropdownValueType)];

            var value = jInput.val();

            // Consturct and return the filter leaf node
            var filterLeafNode = new objects.FilterLeafNode(value, target, operationType, caseSensitivity, filterDataType, booleanOperator, dropdownValueType);

            return filterLeafNode;
        };

        return {
            // Properties and attributes
            ParentElementSelectorAttr: parentElementSelectorAttr,

            // Hook in functions
            LinkFiltersToTable: linkFiltersToTable,

            // Clear Filter Values
            ClearFilterValues: clearFilterValues

        };

    })();

    /*
        Namespace the jquery data tables extensions
    */

    DSS.namespace("query.tables.filter", dataQueryFilterExtensions);

})(jQuery);