// Describes a base ajax entity repository that can be used to retrieve
// Json data for any(generic) domain entities.
(function (dss, $) {
    // The base repositoryt is a 'class' defined over an entity object
    // -- You should be able to create multiple repositories
    // -- for different entities.
    // -- The client code can define an optional area and a controller value
    // -- that will be used to build the routes for the ajax requests.
    var baseRepository = function (entitiy, userArea, userController) {
        // make sure the data types are defined properly
        if (typeof entitiy !== "string") {
            alert("Invalid entitiy parameter type for base repository");
        }

        // check types for the user area value
        if (typeof userArea !== "string" && typeof userArea !== "undefined") {
            alert("Invalid userArea parameter type for base repository");
        }

        // check types for the user controller valuy
        if (typeof userController !== "string" && typeof userController !== "undefined") {
            alert("Invalid userController parameter type for base repository");
        }

        this.entity = entitiy;

        // constuct the ajax route parameters
        var area = userArea || "";
        var controller = userController || entitiy;

        if (area !== "") {
            this.baseRoute = "/" + area + "/" + controller + "/";
        } else {
            this.baseRoute = "/" + controller + "/";
        }
    };

    //
    // GetAll - return all the entities in json format
    baseRepository.prototype.GetAll = function (success, error, urlSuffix) {
        if (typeof urlSuffix === "undefined") {
            urlSuffix = "";
        }

        var url = this.baseRoute + urlSuffix;

        // get an ajax object with an empty data object
        // call the Get action with no data, passing in undefined for the ajax json object
        var ajaxObject = dss.Utilities.Ajax.GetAjaxGet(url, undefined, success, error);

        $.ajax(ajaxObject);
    };

    // 
    // QueryAll - Returns a subset of the entities based on provided query parameters
    baseRepository.prototype.QueryAll = function (queryParams, success, error, urlSuffix) {
        if (typeof urlSuffix === "undefined") {
            urlSuffix = "";
        }

        var url = this.baseRoute + urlSuffix;

        var ajaxObject = dss.Utilities.Ajax.GetAjaxPostObject(url, queryParams, success, error);

        $.ajax(ajaxObject);
    };

    //
    // GetAll - return a single entitiy based on the entitiy id
    baseRepository.prototype.Get = function (id, success, error) {
        // construct the propper url to hit the propper api controller
        // adding the id will trigger single get based on the id in the route.
        var url = this.baseRoute + "/" + id;

        // Create the data object
        var data = {};
        data.id = id;

        // Create the ajax object with the correct data parameter
        var ajaxObject = dss.Utilities.Ajax.GetAjaxGet(url, data, success, error);

        // start the ajax call
        $.ajax(ajaxObject);
    };

    // use the namespacing general function to
    // namespace the base repository class
    dss.namespace("Common.Framework.BaseRepository", baseRepository);

})(window.DSS = window.DSS || {}, jQuery);