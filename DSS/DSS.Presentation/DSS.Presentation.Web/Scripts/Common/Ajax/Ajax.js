(function ($) {
    var ajaxUtility = (function () {
        // Creates a generaj ajax json post object.
        var getAjaxPost = function (url, data, success, error) {
            var ajaxObject = {};

            // data should be a javascript object which will be serialized.
            var jsonData = JSON.stringify(data);
            
            // set the ajax params from the
            // user provided values
            ajaxObject.data = jsonData;
            ajaxObject.url = url;
            ajaxObject.success = success;
            ajaxObject.error = error;

            ajaxObject.type = "POST";

            ajaxObject.contentType = "application/json";
            ajaxObject.dataType = "json";

            return ajaxObject;
        };
        
        var getAjaxGet = function (url, data, success, error) {
            var ajaxObject = {};

            // set the ajax params from the
            // user provided values
            // set the data if only it is not undefined
            // so this can be used to call the GET for all the entities
            if (typeof data !== "undefined") {
                // set the data
                ajaxObject.data = data;
            }
            ajaxObject.url = url;
            ajaxObject.success = success;
            ajaxObject.error = error;

            ajaxObject.type = "GET";

            ajaxObject.contentType = "application/json";
            ajaxObject.dataType = "json";

            return ajaxObject;
        };

        return {
            GetAjaxPostObject: getAjaxPost,
            GetAjaxGet: getAjaxGet
        };
    })();

    // namespace the ajax utlity using the general namespacing function
    DSS.namespace("Utilities.Ajax", ajaxUtility);

})(jQuery);