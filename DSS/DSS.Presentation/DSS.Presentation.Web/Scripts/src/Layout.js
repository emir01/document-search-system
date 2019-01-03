/*
    The general layout module handling basic layout alignment
    Specific for DSS functionality
*/  
    

(function () {
    var layoutModule = (function () {

        /*
            Public functions
            --------------------------------------------------------------------------------
        */

        // the main intitialization function
        var init = function () {
            setupContentResize();
            
            // set the event handler for window resize
            $(window).resize(setupContentResize);
        };

        var setActiveMenuItem = function (menuId) {
            $("#" + menuId).parent("li").addClass("active");
        };

        /*
            Private functions
            --------------------------------------------------------------------------------
        */

        // sets up the main content height and resize functionality
        var setupContentResize = function () {
            var header = $("#header");
            var content = $("#content");
            var body = $("body");

            // calculate the basic height 
            var height = body.height() - header.height();

            // remove the bottom and top margins from the elements
            height = height - parseInt(header.css("margin-bottom"));
            height = height - 2 * (parseInt(content.css("margin-top")));
            height = height - 2 * (parseInt(content.css("padding-top")));

            content.height(height);
        };

        return {
            init: init,
            SetMenuItemActive: setActiveMenuItem
        };
    })();

    // namespace the layout module under the DSS namespace layout object
    DSS.namespace("layout", layoutModule);
})();