/*
    Module used to construct urls as per the MVC routing schemes
*/

(function ($) {
    var uirlHelperUtilities = (function () {

        /*
            Creates a url based on values for the action, controller and area parameters
        */
        
        var getUrl = function(action, controller, area) {
            
            // Check the params
            
            if(typeof action != "string" || action.trim() === "") {
                throw new Error("Cannot create url without the action parameter");
            }
            
            if (typeof controller != "string"  || controller.trim() === "") {
                throw new Error("Cannot create url without the controller parameter");
            }

            var areaProvided = false;
            
            // If the area param is provided it must be a string
            if (typeof area != "undefined" && typeof area != "string") {
                throw new Error("Cannot create url with the wrong area parameter. Area parameter must be string");
            }
            else {
                
                if(typeof  area == "string" && area.trim() !== "")
                    areaProvided = true;
            }
            
            var url = "";
            
            if (areaProvided) {
                url += area.trim();
            }

            //add the controller
            url += "/" + controller.trim();
            
            // add the action
            url += "/" + action.trim();

            return url;
        };
      
        return {
           GetUrl:getUrl
        };
    })();

    // namespace the ajax utlity using the general namespacing function
    DSS.namespace("url", uirlHelperUtilities);

})(jQuery);