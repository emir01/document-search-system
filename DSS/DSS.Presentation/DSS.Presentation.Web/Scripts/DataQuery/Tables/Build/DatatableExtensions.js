(function ($) {
    /*
        jQuery Data Table extensions for working with certain aspects of the grid module.
    */

    var dataTableExtensions = (function () {

        /*
            Table Initialization and configuration functions
        */

        var startTableConfig = function (selector) {
            return new tableConfigObject(selector);
        };

        /*
            Table configuration objet that will contain chainable methods
            that can be used as helpers to fully configure a JQuery Data Tables
            Grid
        */

        var tableConfigObject = function (selector) {
            var that = this;
            that.options = {};
            that.selector = "";

            // Validate the innitial selector beeing set.
            if (typeof selector != "undefined") {
                if (typeof selector != "string") {
                    DSS.query.exception.Fire(DSS.query.exception.Table.ConfiguratorSelectorInvalidFormat, "Initializing a table config object with an invalid selector");
                }
                else {
                    that.selector = selector;
                }
            }

            /*
                Action related properties
            */

            // Flag indicating if an action column is set.
            that.isActionColumnSet = false;

            // The index of the column set as an actoin column
            that.actionColumnIndex = -1;

            // The collection of actions configured for the grid. 
            // Valid only if the isActionColumn flag is set to true
            that.actions = [];


            // Flag indicating if filtering has been set for the given configuration 
            // object
            var isFilteringSet = false;

            // Defines the filter parent element
            var filterParentElementSelector = "";
        };

        /*
            ==========================================================================================================================================
            The configuration object chainable functions used to configure and setup the options object
            used on execution
            ==========================================================================================================================================
        */

        /*  
            Actually execute the configured object calling low level data tables code
            to actually initialize the grid for the given selector. Execute is a terminal call meaning chainability will be broken
            --------------------------------------------------------------------------------------------------------------------------
        */

        tableConfigObject.prototype.execute = function () {

            // The selector has not been properly set throw the exception
            if (this.selector.trim() == "") {
                DSS.query.exception.Fire(DSS.query.exception.Table.MissingSelectorForTableInit, "Trying to execute an table config object without setting the selector");
                return null;
            }
            
            else {

                // Check if filtering is set in which case we will store aditional information
                // on the grid  UI  element via the selector

                if (this.isFilteringSet) {

                    // Check the filter elements selector appropriate type
                    if (typeof this.filterParentElementSelector == "string") {

                        // Store the selector attribute on the grid element
                        $(this.selector).attr(DSS.query.tables.filter.ParentElementSelectorAttr, this.filterParentElementSelector);
                    }
                }

                return $(this.selector).dataTable(this.options);
            }
        };
        
        /*
            Set a callback function for any   of the possible client callbacks for the jquery Data Grid
        */

        tableConfigObject.prototype.setGridCallback = function (callbackName, callbackFunction) {
            
            if (typeof callbackName != "string") {
                DSS.query.exception.Fire(DSS.query.exception.Table.InvalidConfiguratoinMethodParameter, "The setGridCallback callbackName parameter is of an invalid type");
                return null;
            }
            
            if (typeof callbackFunction != "function") {
                DSS.query.exception.Fire(DSS.query.exception.Table.InvalidConfiguratoinMethodParameter, "The setGridCallback callbackFunction parameter is of an invalid type");
                return null;
            }

            // Set the callback functiona for the callback specified with the callback name
            this.options[callbackName] = callbackFunction;
            
            // Return the configuration object to maintain chainability
            return this;
        };
        
        /*
            Set the selector property after the config object has been created. IF the selector
            is in the wrong format an exception is thrown and chainability is broken
            --------------------------------------------------------------------------------------------------------------------------
        */

        tableConfigObject.prototype.setSelector = function (selector) {
            // Validate the set selector value
            if (typeof selector != "undefined") {
                if (typeof selector != "string") {
                    DSS.query.exception.Fire(DSS.query.exception.Table.ConfiguratorSelectorInvalidFormat, "Seting a wrong data type selector");
                    return null;
                }
                else {
                    this.selector = selector;
                    return this;
                }
            }
            else {
                DSS.query.exception.Fire(DSS.query.exception.Table.ConfiguratorSelectorInvalidFormat, "You Must provide a selector string when setting the selector");
                return null;
            }
        };

        /*
            Configure Server side processing by turning it on and off and specifying a url
            Syntatic sugar functions
            --------------------------------------------------------------------------------------------------------------------------
        */

        tableConfigObject.prototype.enableServerProcessing = function (url) {
            // Validate the url
            if (typeof url != "undefined") {

                if (typeof url != "string") {
                    DSS.query.exception.Fire(DSS.query.exception.Table.ServerUrlInvalidException, "Specified invalid url data type when enabling server processing");
                    return null;
                }
                else {
                    this.options.bServerSide = true;
                    this.options.sServerMethod = "POST";
                    this.options.sAjaxSource = url;

                    // We are also going to overide the fnServerData Function to send
                    // the data to the server using json data
                    this.options.fnServerData = DSS.query.tables.server.ProcessTableServerData;

                    return this;
                }
            }
            else {
                // if the url is not set throw an exception
                DSS.query.exception.Fire(DSS.query.exception.Table.ServerUrlInvalidException, "Specified invalid or missing url data type when enabling server processing");
                return null;
            }
        };

        tableConfigObject.prototype.disableServerProcessing = function () {
            // no validation
            this.options.bServerSide = false;
            return this;
        };

        tableConfigObject.prototype.setServerSideUrl = function (url) {
            // Validate the url. The url must be a string value
            if (typeof url != "string") {
                DSS.query.exception.Fire(DSS.query.exception.Tables.ServerUrlInvalidException, "Specified invalid url data type when setting server side url");
                return null;
            }
            else {
                this.options.sAjaxSource = url;
                return this;
            }
        };


        /*
            General grid configuration
            --------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------
        */

        /*
            Sets the default row count for the grid beeing configured

            Params: 
                    rowCount - Number: the default selected row count.
        */

        tableConfigObject.prototype.setDefaultRowCount = function (rowCount) {
            this.options.iDisplayLength = rowCount;
            return this;
        };

        /*
            Set the number and values for the row count selection option on the Data Tables Grids

            Params:
                    rowCountValues - Array : the actual row count selection values, that can be used for both the value and display 
                                            depending if the rowCountText array is defined
                    rowCountText - 
        */

        tableConfigObject.prototype.setRowCountSelection = function (rowCountValues, rowCountText) {
            var aLengthMenu = [];

            aLengthMenu.push(rowCountValues);

            if (typeof rowCountText != "undefined" && rowCountText != undefined) {
                aLengthMenu.push(rowCountText);
            }

            // Set the aLengthMenu param to the configuration object options
            this.options.aLengthMenu = aLengthMenu;

            // Return the configuration object to allow chaining
            return this;
        };

        /*
            Methods used in configuring wether the row count selection menu will be displayed
        */

        // Enable the row count selection menu
        tableConfigObject.prototype.enableRowCountSelection = function () {
            this.options.bLengthChange = true;

            // Returns the configuration object to allow chaining
            return this;
        };

        // Disables the row count selection menu
        tableConfigObject.prototype.disableRowCountSelection = function () {
            this.options.bLengthChange = false;

            // Returns the configuration object to allow chaining
            return this;
        };

        /*
            Extension functions regarding column definitions
            --------------------------------------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------------------------------------
        */

        /*
            The simplest form of column configuration. Sets the column with the given index, linking it to the
            data source property with the given name and setting the specified column width

            Params: 
                    columnIndex - Specifies which table column is configured with this setColumnCall
                    propertyName - Specifies to which property in the data source the currently configured column is linked to
                    width - Specifies the width of the configured column

        */

        tableConfigObject.prototype.setColumn = function (columnIndex, propertyName, width) {

            // the config object
            var rawConfigObject = this.options;

            // check if the config object has the aoColumnDefs object set and if it has not set it
            if (rawConfigObject.aoColumnDefs == undefined) {
                rawConfigObject.aoColumnDefs = [];
            }

            // Create the new column configuration object
            var newColumnConfig = {};

            // Set the target to the specified index
            newColumnConfig.aTargets = [];
            newColumnConfig.aTargets.push(columnIndex);

            // set the bound property name
            newColumnConfig.mData = propertyName;

            // For now just valiate the  width parameter as its optional
            if (typeof width != "undefined") {
                newColumnConfig.sWidth = width;
            }

            // Add the new column configuration object to the grid configuration column definitions
            rawConfigObject.aoColumnDefs.push(newColumnConfig);

            // Maintain chainability
            return this;
        };

        /*
          Extension functions regarding actions buttons on the action column
          --------------------------------------------------------------------------------------------------------------------------
          --------------------------------------------------------------------------------------------------------------------------
        */

        /*
            Sets the specified column as an action column. Links the column with a specific property of the data source.
            The setAction column function must be called and succeeed before beeing able to add actions to the grid

            Params : 
                    columnIndex - Specifies the UI grid column index which we are setting as the action colummn
                    boundPropertyName - Specifies the property name on the data source to which the column will be bound
                    width - Specifies the width for the configured action column
                    */

        tableConfigObject.prototype.setActionColumn = function (columnIndex, boundPropertyName, width) {
            // the config object
            var rawConfigObject = this.options;

            // reference so we can closure it up in the mRender callback setting
            var that = this;

            // check if the config object has the aoColumnDefs object set and if it has not set it
            if (rawConfigObject.aoColumnDefs == undefined) {
                rawConfigObject.aoColumnDefs = [];
            }

            var actionColumnConfig = {};

            actionColumnConfig.aTargets = [];
            actionColumnConfig.aTargets.push(columnIndex);

            // set the bound property name
            actionColumnConfig.mData = boundPropertyName;

            // For now just valiate the  width parameter as its optional
            if (typeof width != "undefined") {
                actionColumnConfig.sWidth = width;
            }

            // setup the rendering function for the actoins for the column
            // pass in aditional properties closuring it up
            actionColumnConfig.mRender = (function (boundValue, type, dataContext) {
                return DSS.query.tables.actions.RenderRowActions(that, boundValue, type, dataContext);
            });

            // Setp action column specific values

            actionColumnConfig.bSortable = false;
            actionColumnConfig.bSearchable = false;

            rawConfigObject.aoColumnDefs.push(actionColumnConfig);

            // Set the apropriate flags
            this.isActionColumnSet = true;
            this.actionColumnIndex = columnIndex;

            // Return the configuration object to maintain chainability
            return this;
        };

        /*
            Adds a new configured action button to the preconfigured action column.

            Params: 
                    - displayText - String | Function, determine the "label" text on the action button
                    - displayTextFlag - Boolean, flag indicating if the dispalyText should be displayed on the button or used as a title only
                    - buttonCssClass -  String, the general custom class string for the action button
                    - buttonIconClasss String | Function, determine the icon class if any to be used on the 
                    - handler Function, the event handler that will be atached to the action

        */

        tableConfigObject.prototype.addAction = function (displayText, displayTextFlag, buttonCssClass, buttonIconClass, handler) {
            if (this.isActionColumnSet) {

                // if the action column is set we are only going to add an action config object to the
                // grid configuration

                var configurationObject = new DSS.query.tables.actions.ActionConfigObject(displayText, displayTextFlag, buttonCssClass, buttonIconClass, handler);

                this.actions.push(configurationObject);

                // Return the configuration obejct to maintain chainability
                return this;
            }
            else {
                // throw an exception
                DSS.query.exception.Fire(DSS.query.exception.Table.ConfiguratorSelectorInvalidFormat, "Trying to add an action to the grid with no configured action column");
                return null;
            }
        };

        /*
            Hook ins to the Table Configuration Object to the Query Filter extensions and functionality
            --------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------
        */

        /*
            Add the filtering server side functionality to the table configuration object via 
            the Query Filter Extensions

            Params:
                    - filtersParentSelector, the jQuery selector for the top most parent element
                                            of the filtering elements on the UI
        */

        tableConfigObject.prototype.filtering = function (filtersParentSelector) {

            // Call the Link Filters to table functionality
            DSS.query.tables.filter.LinkFiltersToTable(filtersParentSelector, this);

            // return the configuration object to maintain chainability
            return this;
        };


        return {
            StartTableConfig: startTableConfig,
        };

    })();

    /*
        Namespace the jquery data tables extensions
    */

    DSS.namespace("query.tables.build", dataTableExtensions);

})(jQuery);