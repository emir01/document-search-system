
/*
 * Define a collection of observable custom extends to be used in the application.
 */
(function (dss, $) {
    
    /*
     * For a given observable, either property or array adds a subscription 
     * that will log the value of the observable after it has been changed.
     */

    ko.extenders.logChange = function (observable, option) {
        observable.subscribe(function (newValue) {
            console.log(option + " value changed to :" + JSON.stringify(newValue));
        });
    };

})(window.DSS = window.DSS || {}, jQuery)