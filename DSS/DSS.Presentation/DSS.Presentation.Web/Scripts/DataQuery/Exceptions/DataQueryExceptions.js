(function ($) {

    /*
        Contains exception module functionality for the Data Query Client Side Module
    */
    
    var dataQueryExceptions = (function () {
        
        /*
            Exception module configuration flags
        */

        var alertBeforeException = true;
        
        /*
            General Data Query exception handling functions
            ============================================================================
        */
        
        /*
            Fire throws an exception with the given name and message. The method will check if the 
        */

        var fire = function (eName, eMessage) {

            if (alertBeforeException) {
                var alertText = "Exception : " + eName + " \n \n";
                alertText += "Message : " + eMessage;

                alert(alertText);
            }
            
            throw new dataTableError(eName, eMessage);
        };
        
        /*
            Custom exception object used in the Data Query client side functionality
        */

        var dataTableError = function(eName, eMessage) {
            this.name = "MyError";
            this.message = eMessage || "Default Message";
        };
        
        dataTableError.prototype = new Error();
        dataTableError.prototype.constructor = dataTableError;;
        
        /*
            Collection of data query module exception names for the Data Query Tables Sub Module
        */
        
        var tableExceptionNames = {
            // Selector exceptions
            ConfiguratorSelectorInvalidFormat: "TableConfiguratorSelectorInvalidFormat",
            MissingSelectorForTableInit: "MissingSelectorForTableInitialization",
            
            // Configuration methods invalid params
            InvalidConfiguratoinMethodParameter:"InvalidConfigurationMethodParameters",
            
            ServerUrlInvalidException: "SpecifiedServerURUrlIsNotDefined",
            
            AddActionUnexpectedConfigException:"InvalidConfiguratoinForActionColumn"
        };
        
        return {
            Fire: fire,
            Table: tableExceptionNames,
        };
        
    })();

    /*
        Namespace the jquery data tables exception functionality
    */
    
    DSS.namespace("query.exception", dataQueryExceptions);
    
})(jQuery);