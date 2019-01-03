// A custom callback object that can be used to call a single callback function once multiple ajax calls finish
// This is code copied from http://stackoverflow.com/questions/4368946/javascript-callback-for-multiple-ajax-calls
(function () {
    var myRequestsCompleted = (function () {
        
        //var numRequestToComplete, requestsCompleted, callBacks, singleCallBack;

        /*
            Ajax callback coinstructor function
        */
        
        return function (options) {
            /*
                Properties and options processing
                =============================================================================================
                =============================================================================================
            */
            
            /*
                Set the default options object if the options object is not provided when
                constructing the ajax request object
            */
            
            if (!options) options = {};

            /*
                Ajax Request properties
            */
            
            this.numRequestToComplete = options.numRequest || 0;
            
            this.requestsCompleted = options.requestsCompleted || 0;
            
            this.callBacks = [];
            
            /*
                If a single callback property is provided in the options 
                parameter add it to the array of callbacks which is used internally
            */
            
            if (options.singleCallback) {
                this.callBacks.push(options.singleCallback);
            }

            // Internal object reference for private functions
            var that = this;
            

            /*
                Internals functions
                =============================================================================================
                =============================================================================================
            */
            
            /*
                Public functions
                =============================================================================================
                =============================================================================================
            */

            /*
                Notify the custom callback object that a request has been finished
            */
            this.requestComplete = function (isComplete) {
                if (isComplete && !checkInternalConditionForCallbackFire()) {
                    this.requestsCompleted++;
                }
                
                if (checkInternalConditionForCallbackFire()) {
                    fireCallbacks();
                }
            };
            
            /*
                Add a single callback function after the ajax request is constructed
            */
            
            this.setCallback = function (callback) {
                callBacks.push(callback);
            };
            
            /*
                Add a callback function to the callbacks queue on a completed request, incrementing the 
                requests completed value by one
            */

            this.addCallbackToQueue = function (isComplete, callback) {
                if (isComplete && !checkInternalConditionForCallbackFire()) {
                    this.requestsCompleted++;
                }

                if (callback) {
                    this.callBacks.push(callback);
                }
                
                if (checkInternalConditionForCallbackFire()) {
                    fireCallbacks();
                }
            };
            
            /*
                Internal functions
                =============================================================================================
                =============================================================================================
            */
            
            /*
                Check if the internal condition to fire the callbacks is satisfied and the callbacks can fire
            */
            
            var checkInternalConditionForCallbackFire = function() {
                return that.requestsCompleted >= that.numRequestToComplete;
            };
            
            /*
                Fires all the provided callback functions when all the requests are finished
            */

            var fireCallbacks = function () {
                for (var i = 0; i < that.callBacks.length; i++) that.callBacks[i]();
            };
        };
    })();
    
    // namespace the ajax callback objerct using the general namespacing function
    DSS.namespace("Utilities.AjaxCallback", myRequestsCompleted);
})();
