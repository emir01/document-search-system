/*
    Client side error processor module that is to be used for error reporting
    error handling and server reporting and logging.

    Module will hook itself to the global ajax error callback. 

    Recommended usage in failed bussines logic on client or anywhere where something unexpected happens.

    #NOTE: Currently not fully implemented as not a priroity but important to have the tm.error.log function which
    can still be used and implemented after
*/

(function (dss, $) {

    var errorProcessor = (function () {

        /*
           Properties
           ===============================================================
       */

        /*
            The types of errors the module will handle/process
        */

        var errorTypes = {
            GeneralError: "GeneralError",

            AjaxErrorCallback: "AjaxErrorCallback",
            ValidationError: "ValidationError",

            JavascriptException: "JavascriptException",
            ResourceNotFound: "ResourceNotFound",

            ServerFailedResponse: "ServerFailedResponse"
        };

        /*
            Initialization
            ===============================================================
        */

        /*
            The main module initialization function
        */

        var init = function () {
            window.onerror = handleWindowOnError;
        };

        /*
            Public functionality
            ===============================================================
       */

        /*
            The most general error logging and processing client function.

            Type - The type of error we are going to be processing, usually pulled from the internal types registry

            Data - Any information assosiated with the error
            
        */

        var log = function (type, data) {
            // TODO : IMPLEMENT
            // TODO : User Logging to log to server
            console.log("Logging error of type: " + type + "! Implement me!");
        };

        /*
            Private utilities
            ===============================================================
       */

        var handleWindowOnError = function (errorMessage, url, lineNumber) {
            dss.lg.log("------------------------------------------------", dss.lg.ns.ErrorProcessor);
            dss.lg.log("Error Message " + errorMessage, dss.lg.ns.ErrorProcessor);
            dss.lg.log("Url " + url, dss.lg.ns.ErrorProcessor);
            dss.lg.log("Line Number " + lineNumber, dss.lg.ns.ErrorProcessor);
            dss.lg.log("------------------------------------------------", dss.lg.ns.ErrorProcessor);

            return false;
        };

        /*
            RMP
            ===============================================================
        */

        return {
            // initialization
            Init: init,

            // public
            Log: log,

            // Properties
            Type: errorTypes
        };
    })();

    // Namespace the module
    // short namespace for easy access
    dss.namespace("error", errorProcessor);

})(window.DSS = window.DSS || {}, jQuery);


