/*
    Contains javascript parameter/object checking <utility></utility> functionality.
*/

(function ($) {

    var params = (function () {
        
        /*
            General Checks
            ========================================================================================
            ========================================================================================
        */

        /*
            Check if a given parameter is defined
        */

        var isDefined = function (parameter) {
            return (typeof parameter !== "undefined" && parameter != undefined);
        };

        /*
            Check if a given parameter is defined and has a non null value asigned
        */

        var isDefinedAndHasValue = function (parameter) {
            return (typeof parameter !== "undefined" && parameter != undefined && parameter != null);
        };
        
        /*
            String Checks
            ========================================================================================
            ========================================================================================
        */

        /*
            Check if a string value parameter is fully defined and if its not an empty string
        */

        var stringIsFullyDefined = function (parameter) {
            if (typeof parameter === "string" && parameter.trim() != "") {
                return true;
            }
            else {
                return false;
            }
        };
        
        /*
            Object Checks
            ========================================================================================
            ========================================================================================
        */
        
        /*
            Check if a given parameter is a non null object
        */
        var isObject = function (parameter) {
            return (typeof parameter === "object" && parameter != null);
        };

        /*
            Check if a given object has all the properties as defined in the propNamesArray
        */

        var objectHasProperties = function (parameter, propNamesArray) {
            if (!isObject(parameter)) {
                return false;
            }

            var globalFlag = true;
            
            for (var i = 0 ; i < propNamesArray.length; i++) {

                var localFlag = false;
                var requestedParameterProperty = propNamesArray[i];

                // check all the properties in the parameter object
                for (var key in parameter) {
                    if (parameter.hasOwnProperty(key)) {
                        // compare the key and the requested parameter
                        if (requestedParameterProperty == key) {
                            localFlag = true;
                        }
                    }
                }

                globalFlag = localFlag;

                // if global flag ever ends up false exit with a false status
                if (!globalFlag) {
                    break;
                }
            }

            // return the status
            return globalFlag;
        };

	    /*
            Check if a given object has the given single property namer
        */
	    
        var objectHasProperty = function (parameter, propertyName) {
            if(!isObject(object)) {
                return false;
            }

            return object.hasOwnProperty(propertyName);
        };
        
        /*
            Function Checks
            ========================================================================================
            ========================================================================================
        */

        /*
            Check if a given parameter is a non null function  
        */

        var isFunction = function (parameter) {
            return (typeof parameter === "function");
        };
        
	    /*
            RMP
            ========================================================================================
            ========================================================================================
        */

        return {
            // General Checks
            isDefined: isDefined,
            isDefinedAndHasValue: isDefinedAndHasValue,
            
            //String Checks
            stringIsFullyDefined:stringIsFullyDefined,

            // Parameter checks related to objects
            isObject: isObject,
            objectHasProperties: objectHasProperties,
            objectHasProperty: objectHasProperty,
            
            // Function parameter related checks
            isFunction: isFunction,
        };
    })();

	DSS.namespace("params", params);
    
})(jQuery);
