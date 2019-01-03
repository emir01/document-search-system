(function ($) {
    // Create a loadmask wrapper around the loadmask basic functionality.
    // Currently workds only with whole html element masking
    var loadmaskWrapper = (function () {

        /*
            Add a mask to the global html element
        */
        
        var mask = function (msg) {
            var loadingMessage = "Loading...";
            if (typeof msg === "string") {
                loadingMessage = msg;
            }

            $("html").mask(loadingMessage);
        };

        /*
            The ClearHtml call that removes the mask from the global html element
        */
        
        var clear = function() {
            $("html").unmask();
        };
        
        /*
            Masks dom elements specified by a jquery selector
        */
        var maskSpecific = function (jElement, message) {
            var loadingMessage = "Loading...";
            
            if(typeof  message === "string") {
                loadingMessage = message;
            }
            
            jElement.mask(loadingMessage);
        };
        
        /*
            Clear the mask from specific dom elements given with the jQuery 
            selector
        */

        var clearSpecific = function(jElement) {
            jElement.unmask();
        };

        // return the actual loadmask wrapper object
        return {
            MaskHtml: mask,
            ClearHtml: clear,
            MaskSpecific: maskSpecific,
            ClearSpecific:clearSpecific
        };
    })();

    // namespace the loadmask wrapper
    DSS.namespace("DSS.load", loadmaskWrapper);
})(jQuery);
