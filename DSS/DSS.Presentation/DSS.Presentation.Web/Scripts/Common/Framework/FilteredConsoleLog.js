/*
    General configurable client side logging and message output module.

    Used during development to output structured messages to the console from variousmodules/

    Message output is controlled with a log flag and special flags for each functionality namespace.

    Uses the GlobalMessage logger to write the messages.
*/

(function (dss, $) {

    var logger = (function () {

        /*
           Properties
           ===============================================================
       */

        /*
            Global log flag. If set to false no console.log calls will ever be made
        */
        var logFlag = true;

        /*
            Logging namespace definitions. Static object containing the namespace definitions.
            Changing this from client code will not change namespace flags, stored in another private object
        */

        var logingNamespaceDefinitions = {
            General: "General",
            ErrorProcessor: "ErrorProcessor"
        };

        /*
            Logging namespace control flags based on the logging namespace definition.
        */
        var loggingNamespaceFlags = {
            "General": true,
            "ErrorProcessor": true
        };

        /*
            Initialization
            ===============================================================
        */

        /*
            The main module initialization function
        */

        var init = function () {

        };

        /*
            Public functionality
            ===============================================================
       */

        /*
            Basic console writer which is flag  and namespace controlled.

            Message namespacing allows us to turn off only specific aspects of logging, for example, 
            we can turn off messages asosiated with table construction but leave everything else on.

            Params:

            message     - What to write to console.
            namespace   - Message namespace w
        */

        var log = function (message, namespace) {

            if (logFlag) {

                // check namespace
                // if the namespace param is defined  we are checking control flags
                if (dss.params.isDefinedAndHasValue(namespace)) {

                    if (dss.params.isDefinedAndHasValue(loggingNamespaceFlags[namespace]) && loggingNamespaceFlags[namespace]) {
                        console.log(message);
                    } else {
                        return null;
                    }
                }
                    // if namespace not defined we are logging
                else {
                    console.log(message);
                }
            }
                // ultimate not logging if the logFlag is false
            else {
                return null;
            }

        };

        /*
            Private utilities
            ===============================================================
        */

        /*
            RMP
            ===============================================================
        */

        return {
            Init: init,

            // Logging
            log: log,

            // NAmespacing
            ns: logingNamespaceDefinitions
        };
    })();

    // Namespace the module
    dss.namespace("logger", logger);

    // shor namespace reference
    dss.namespace("lg", logger);

})(window.DSS = window.DSS || {}, jQuery);