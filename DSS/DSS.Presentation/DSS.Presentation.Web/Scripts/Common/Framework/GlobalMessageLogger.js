/*
    Logging module that can be used to write messages to the console. The writing of the messages is controlled
    by configuration flags on the server.

    Whatever is logged is passed to the server if the configuration is valid.

    NOTE: It is used by the client side filtered console logger to actually write to the console.
*/
(function ($, undefined) {
    var loggingModule = (function () {
        
        // the flag that tells us if we will output logging information 
        // to the client browser console. The "Was" Property is used for toggling in the disable/enable functions.
        var consoleLoggingEnabled = true;
        var consoleLoggingWasEnabled = true;

        // the flag that tells us if we will send logged information to the
        // server to be handled/stored. The "Was" property is used in toggling in the disable/enable functoins.
        var serverLoggingEnabled = false;
        var serverLoggingWasEnabled = false;

        // The url to where the logging information will be passed if server logging is enabled
        var serverLoggingUrl = undefined;

        // The default url from where to get server configured logging information
        var defaultServerConfigurationConfigUrl = "/Logging/GetLoggingConfiguration";

        // the general flag indicating the module was initialized successfully
        var initOk = false;

        //============================================================
        //================== Public Functions ========================
        //============================================================
        // the main intitialization function
        // for the log module, that should initialy talk to the server
        // and figure out client side logging settings.
        // Params: 
        // -- serverConfigurationUrl - the url from where we will retrieve logging settings
        // -- serverLogUrl - the url to where, if enabled, we will send logging information
        var init = function (serverConfigurationUrl, serverLogUrl) {
            //check for requirments
            initOk = checkModuleRequirments();

            // if the requirment check has failed stop the initialization step
            if (!initOk) {
                alert("Logging Module initialization failed");
                return false;
            }

            // make the call to get the configuration settings if the server configuration url is passed
            // in as a parameter
            if (typeof serverConfigurationUrl === "string" && serverConfigurationUrl != "") {
                defaultServerConfigurationConfigUrl = serverConfigurationUrl;
            }
            getServerLoggingConfiguration();

            // if the server log url is a valid passed in parameter
            if (typeof serverLogUrl === "string" && serverLogUrl !== "") {
                serverLoggingUrl = serverLogUrl;
            }
        };

        // The main user logging function 
        var write = function (message, data) {
            if (initOk) {
                // if writing to console is enabled
                // the default setting is that the console logging is enabled
                if (consoleLoggingEnabled) {
                    writeToConsole(message, data);
                }

                if (serverLoggingEnabled) {
                    sendToServer(message, data);
                }
            }
        };

        // disables all enabled logging processes
        var disable = function () {
            // disable console logging but first store the previous state.
            consoleLoggingWasEnabled = consoleLoggingEnabled;
            consoleLoggingEnabled = false;

            // disable server logging but first store the previous state.
            serverLoggingWasEnabled = serverLoggingEnabled;
            serverLoggingEnabled = false;
        };

        // returns the logging flags to the previous condition
        var enable = function () {
            // return the console logging flag to its previous state.
            consoleLoggingEnabled = consoleLoggingWasEnabled;

            // return the server logging flag to its previous state.
            serverLoggingEnabled = serverLoggingWasEnabled;
        };

        // Client side function that enables us to set the flag for console logging.
        var setConsoleLoggingState = function (state) {
            // dont change the state if the state parameter is not a valid boolean
            if (typeof state !== "boolean") {
                return;
            }
            else {
                consoleLoggingEnabled = state;
            }
        };

        // Client side function that enables us to set the flag for server logging.
        // Note : Server logging might not occur even if the state is true because of properly set
        //        server logging urls
        var setServerLoggingState = function (state) {
            // dont change the state if the state parameter is not a valid boolean
            if (typeof state !== "boolean") {
                return;
            }
            else {
                serverLoggingEnabled = state;
            }
        };

        //============================================================
        //================== Private Functions ========================
        //============================================================

        // The function that writes to the console
        var writeToConsole = function (message, data) {
            var timeStamp = $.now();
            console.log("Log (" + timeStamp + "): " + message);

            if (data != undefined) {
                console.log("Log (" + timeStamp + "): " + data.toString());
            }
        };

        // The function that sends the log information to the server
        var sendToServer = function (message, object) {
            // only send server information if the url is set and server logging is
            // enabled
            if (typeof serverLoggingUrl === "string" && serverLoggingEnabled) {
            }
        };

        // The function that retrieves server configuration information
        var getServerLoggingConfiguration = function (configurationUrl) {
            if (initOk) {
                // make an ajax call to the server to retrieve server logging information
                var ajaxParams = {};
                ajaxParams.type = "POST";
                ajaxParams.url = defaultServerConfigurationConfigUrl;
                ajaxParams.contentType = "application/json";
                ajaxParams.dataType = "json";
                ajaxParams.dataType = "json";

                ajaxParams.success = function (data) {
                    // data should contain the configuration values
                    serverLoggingEnabled = data.ServerEnabled;
                    consoleLoggingEnabled = data.ConsoleEnabled;
                    serverLoggingUrl = data.ServerLoggingUrl;
                };

                ajaxParams.error = function (object) {
                    // if we did not get the server information for server logging
                    // we will disable server logging
                    serverLoggingEnabled = false;
                };

                // make the ajax request
                $.ajax(ajaxParams);
            }
        };

        // The function that checks for the needed requirments
        var checkModuleRequirments = function () {
            // check for the jquery requirment
            if (jQuery == undefined) {
                alert("jQuery is required for the logging module. Please import jQuery before the logging module.");
                return false;
            }

            return true;
        };

        //============================================================
        //================== Revealing module =======================
        //============================================================

        return {
            init: init,
            write: write,
            disable: disable,
            enable: enable,
            setConsoleState: setConsoleLoggingState,
            setServerLState: setServerLoggingState
        };
    })();

    // namespace the logging module 
    DSS.namespace("log", loggingModule);

})(jQuery);
