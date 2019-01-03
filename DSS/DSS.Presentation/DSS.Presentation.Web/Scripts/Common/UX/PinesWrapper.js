// Wrapper around the Pines Notify Plugin to simplify the creation of some basic messages
(function ($) {
    // Create the pines wrapper object
    var pinesWrapper = function () {
        
        // The initialization function
        var init = function() {
            // check if jquery and notify have been loaded
        };

        // Display a success pines notify message
        var success = function (title, message) {
            $.pnotify({
                title: title,
                text: message,
                type: 'success'
            });
        };

        // Display a regular info pines notify message
        var info = function (title, message) {
            $.pnotify({
                title: title,
                text: message,
                type: 'info'
            });
        };

        // Display a error pines notify message
        var error = function (title, message) {
            $.pnotify({
                title: title,
                text: message,
                type: 'error'
            });
        };

        // define the public functions for the pines wrapper
        return {
            init:init,
            success: success,
            info: info,
            error: error
        };
    }();

    // namespace the pines wrapper in a short 
    // namespcace under  DSS.msg
    DSS.namespace("msg", pinesWrapper);
})(jQuery);
