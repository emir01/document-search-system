/*
    Contains jQuery Datatables Server Communication functionality and utilities
*/

(function ($) {

    var dataTablesServerComm = (function () {

        /*
              Shorthand references
              --------------------------------------------------------------------------------------------------------------
              --------------------------------------------------------------------------------------------------------------
        */

        /*
            Public interface and hook ins
            --------------------------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------------------------
       */

        /*
            The jQuery Data tables fnServerData custom method that will send the grid data
            in a JSON POST Method
        */
        var processServerTableData = function (sSource, aoData, fnCallback, oSettings) {

            // Preprocess the data that is sent to the server
            var dataTablesParam = getDataTablesParamObject(aoData);
            
            var filters = getDataTalbesFiltersObject(aoData);

            // Construct the data object that will be stringified
            var allServerParams = {};
            allServerParams.dataTablesParam = dataTablesParam;
            allServerParams.filters = filters;

            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "contentType": "application/json",
                "type": "POST",
                "url": sSource,
                "data": JSON.stringify(allServerParams),
                "success": fnCallback
            });
        };


        /*
            Private POST/JSON Object consturctors
            --------------------------------------------------------------------------------------------------------------
            --------------------------------------------------------------------------------------------------------------
       */

        /*
            Creates the Data Tables Regular Parameters object that can be stringified
            and sent via JSON POST to the server side functionality

            Params: aoData - the data passed in from the underlying Data Tables functionality
        */

        var getDataTablesParamObject = function (aoData) {
            var dataTableParams = {};

            for (var kk = 0; kk < aoData.length; kk++) {

                // Get the parameter and the name and value for the parameter
                var parameter = aoData[kk];

                var paramName = parameter.name;
                var paramValue = parameter.value;

                // Check if this is the custom filters parameter and do not process/add it
                if (paramName === "filters") {
                    continue;
                }

                // check if this parameter is supposed to go 
                // in an array based on the '_' character in the name
                // the '_' cannot be the first char as per the Data Tables spec

                if (paramName.indexOf('_') > 0) {

                    // get the name of the parameter array from the
                    // parameter name
                    var paramArrayName = paramName.split('_')[0];

                    // Define the array if it has not been defined already
                    // Check using the 'a' character in front because of name collisions
                    if (dataTableParams["a" + paramArrayName] == undefined) {
                        dataTableParams["a" + paramArrayName] = [];
                    }

                    dataTableParams["a" + paramArrayName].push(paramValue);

                    continue;
                }

                // otherwise just add the value
                dataTableParams[paramName] = paramValue;
            }

            return dataTableParams;
        };

        /*
           Creates the Data Tables Filter Values Parameters object that can be stringified
           and sent via JSON POST to the server side functionality if such object is specified

           Params: aoData - the data passed in from the underlying Data Tables functionality
       */

        var getDataTalbesFiltersObject = function (aoData) {
            // name of the param is customFilters
            // just return the custo filters object
            var customFilters = null;
            
            for(var kk =0; kk < aoData.length; kk++) {
                var aoDataObject = aoData[kk];
                
                if (aoDataObject.name === "filters") {
                    customFilters = aoDataObject.value;
                }
            }
            
            return customFilters;
        };

        return {
            ProcessTableServerData: processServerTableData,
        };

    })();

    /*
        Namespace the jquery data tables extensions
    */

    DSS.namespace("query.tables.server", dataTablesServerComm);

})(jQuery);