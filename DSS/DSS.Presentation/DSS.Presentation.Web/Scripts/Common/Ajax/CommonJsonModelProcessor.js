(function (dss, $) {

    var jsonModelProcessor = (function () {

        /*
            Properties
            ========================================================
        */

        var debugMode_Exceptions = true;

        /*
            Public functions
            ========================================================
        */

        /*
            Process a server side serialized 
        */

        var process = function (jsonModel, dataCallback) {
            // if the status is set to true
            if (jsonModel.Status) {
                dataCallback(jsonModel.Data);

            } else {
                // check if an exception happened
                if (debugMode_Exceptions && jsonModel.IsException) {
                    dss.msg.error("Exception", jsonModel.ExceptionMessage);
                } else {
                    dss.msg.error("Error", jsonModel.Message);
                }
                
                dataCallback(null);
            }
        };

        /*
            Private utilities
            ========================================================
        */


        /*
            Revealing module pattern
            ========================================================
        */
        return {
            process: process
        };
    })();

    // namespace the utility
    dss.namespace("Utilities.json", jsonModelProcessor);
    dss.namespace("json", jsonModelProcessor);

})(DSS = window.DSS || {}, jQuery);