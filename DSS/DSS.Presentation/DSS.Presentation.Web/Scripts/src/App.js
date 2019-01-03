// The main initialization module that handles initial javascript initiazation
// for many of the plugins
(function () {
    var mainInit = (function () {
        // the main init function.
        var init = function () {
            // call the init on the layout module
            DSS.layout.init();
    
            // call the init on the logging module, for initial logging setup
            DSS.log.init();
        };

        return {
            Initialize: init
        };
    })();
    
    // namespace the main initialization module
    DSS.namespace("DSS.Init.Main", mainInit);
})();
