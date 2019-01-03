/*
    The query string utility module is used to ease up the working with the query string 
    and general url manipulation regarding storing search information. 
*/

(function ($) {
    var urlSearchUtility = (function () {

        /*
           Properties and configuration
           ====================================================================================
        */

        /*
            The default connector character between multiple key value pairs
        */

        var CONNECTOR = "&";

        /*
            The default asigner character for linking the key and the value
        */

        var ASIGNER = "=";

        /*
            The default character used to split the index from a given keyword
            in the case of treating keywords like 
        */

        var INDEX_SPLITTER = "_";

        /*
            Public Query Search Methods
            ====================================================================================
            ====================================================================================
        */

        //#region Public

        /*
            Add a hash url key/value parameter, enabling us to store possible actions
            without reloading the page

            Params:

                - key,              The key for the parameter

                - value,            The value for the parameter

                - update,           If the key is found should we update the value
        */

        var writeHashParameter = function (key, value, update) {
            // get the hash objects array
            var hashObjectArray = getHashObjectsArray();

            // check if the key is already present

            if (keyPresentInHashArray(key, hashObjectArray)) {

                // if the key is present we will be checking
                // if the update field is set and should we update 
                // the key or add a new value

                if (typeof update == "boolean" && update) {

                    // update the hash object array and return the new window location.hash value
                    updateExistingValueForKeyInObjectArray(hashObjectArray, key, value);
                    return updateWindowHash(hashObjectArray);
                }

                // only add the new value for the key if its not been
                // added before

                if (!valuePresentInHashArray(value, hashObjectArray)) {

                    // just regually add the new KeyValue Object
                    addNewKeyValueObjectToArray(key, value, hashObjectArray);
                    return updateWindowHash(hashObjectArray);
                }
            }
            else {
                // if the key is not present just add the new key
                // value object
                addNewKeyValueObjectToArray(key, value, hashObjectArray);
            }

            // GET A PROCESSED HASH STRING FROM THE HASH OBJECTS ARRAY
            return updateWindowHash(hashObjectArray);
        };

        /*
            Removes a url hash parameter for the given key without reloading
            the page. Removes all values for a given key for keys with multiple values.

            -key    The key of the hash paramter to be removed. If a specific index is not provided
                    we will remove all the key value pairs for the given key.
                    */

        var removeHashParameterByKey = function (key) {
            return removeHashObjectForCondition(function (object) {
                return object.key == key;
            });
        };

        /*
         * Remove a hash parameter based on the key/value provided. 
         * 
         * Will remove a single entry that for a given key if it matches both the key and value for that key at least one.
         */

        var removeHashParameterByKeyValuePair = function (key, value) {
            return removeHashObjectForCondition(function (object) {
                return object.key == key && object.value == value;
            });
        };

        /*
         * Private hash object removal function that will update the hash string based on a removal 
         * condition for the hash objects
         */

        var removeHashObjectForCondition = function (conditionCheck) {
            var hashObjectArray = getHashObjectsArray();

            for (var i = 0; i < hashObjectArray.length; i++) {
                var hashObject = hashObjectArray[i];

                if (conditionCheck(hashObject)) {
                    hashObjectArray.splice(i, 1);
                    hashObject = null;
                }
            }

            var updatedHashString = getHashStringFromObjects(hashObjectArray);

            window.location.hash = updatedHashString;

            return true;
        };

        /*
            Returns a hash value or values for a given hash key
        */

        var getHashValueForKey = function (key) {
            // get the hash objects array
            var hashObjectArray = getHashObjectsArray();

            var valueArray = [];

            // populate the value Array with any hash objects with a matching key
            for (var i = 0; i < hashObjectArray.length; i++) {
                var hashObject = hashObjectArray[i];

                if (hashObject.key == key) {
                    valueArray.push(hashObject.value);
                }
            }

            // if there are no values return null
            if (valueArray.length == 0) {
                return null;
            }

            // TODO : not clear on the purpose of this check
            if (valueArray.length == 1) {
                return valueArray[0];
            }

            // if there is more than value return the entire array
            return valueArray;
        };

        /*
            Clears all the hash key/value combinations added to the url
        */

        var clearAllHashParameters = function () {
            // make a new empty hash object array
            var hashObjectArray = [];
            updateWindowHash(hashObjectArray);

            return true;
        };

        /*
            Returns a flag indicating if a given key value pair is contained in the hash array
            
            Params
        */

        var containsKeyValueCombination = function (key, value) {

        };

        //#endregion

        /*
            Private utilities
            ====================================================================================
            ====================================================================================
        */

        //#region Private

        /*
            Get the hash string from the array of hash elements and update the window
            url hash string 
        */

        var updateWindowHash = function (hashObjectArray) {
            var updatedHashString = getHashStringFromObjects(hashObjectArray);
            window.location.hash = updatedHashString;

            return true;
        };

        /*
            Adds the new key value parameter to the  array of hash objects

            - key       The key of the new hash key value that will be added in the array
            - value     The value for the new hashk key val
        */

        var addNewKeyValueObjectToArray = function (key, value, array) {
            var object = {};

            object.key = key;
            object.value = value;

            array.push(object);
        };

        /*
            Check if a hash value is present in the key value hash object array
        */

        var valuePresentInHashArray = function (value, hashObjectArray) {
            // if we did not pass an array the value won't be in the array
            if (!isArray(hashObjectArray)) {
                return false;
            }

            // if the array has no objects the value is not in the array
            if (hashObjectArray.length == 0) {
                return false;
            }

            // checks if the passed in value is in the hashObjectArray
            for (var i = 0; i < hashObjectArray.length; i++) {
                var object = hashObjectArray[i];

                if (object.value == value) {
                    return true;
                }
            }

            return false;
        };

        /*
            Check if a hash key is present in the key value hash object array
        */

        var keyPresentInHashArray = function (key, hashObjectArray) {
            // if we did not pass an array the value won't be in the array
            if (!isArray(hashObjectArray)) {
                return false;
            }

            if (hashObjectArray.length == 0) {
                return false;
            }

            // checks if the passed in value is in the hashObjectArray
            for (var i = 0; i < hashObjectArray.length; i++) {
                var object = hashObjectArray[i];

                if (object.key == key) {
                    return true;
                }
            }

            return false;
        };

        /*
            Used to update a hash value for a given key. If there are multiple values for the key
            all the values will be set to the new value.
        */
        var updateExistingValueForKeyInObjectArray = function (hashObjectArray, key, newValue) {
            if (!isArray(hashObjectArray)) {
                return false;
            }

            for (var i = 0; i < hashObjectArray.length; i++) {
                var object = hashObjectArray[i];

                // it will update all the values for all the keys
                // if there a
                if (object.key == key) {
                    object.value = newValue;
                }
            }

            return true;
        };

        /*
            Gets a hash string from a collection of hash objects
        */

        var getHashStringFromObjects = function (hashObjectsArray) {
            if (!isArray(hashObjectsArray)) {
                return "";
            }

            var hashString = "";

            for (var i = 0; i < hashObjectsArray.length; i++) {
                var object = hashObjectsArray[i];
                var keyValuePair = getKeyValueStringFromObject(object);

                // for every other hash object except the first 
                // add a connector
                if (i != 0) {
                    keyValuePair = CONNECTOR + keyValuePair;
                }

                hashString = hashString + keyValuePair;
            }

            return hashString;
        };


        /*
            Get an array of key/value hash objects from the current window location hash.
            Accesses the window.location.hash string property
        */

        var getHashObjectsArray = function () {
            // gets the current hash string
            var currentHash = window.location.hash;

            if (typeof currentHash == "undefined") {
                currentHash = "";
            }

            // to remove the starting # character
            if (currentHash != "") {
                currentHash = currentHash.substring(1, currentHash.length);
            }

            // split the hash on the default connector
            var splitHashStringArray = currentHash.split(CONNECTOR);

            // check if have gotten a propper array
            if (!isArray(splitHashStringArray)) {
                return [];
            }

            var hashObjectArray = [];

            for (var i = 0; i < splitHashStringArray.length; i++) {
                var hashObject = getHashObjectFromString(splitHashStringArray[i]);

                if (hashObject != null) {
                    hashObjectArray.push(hashObject);
                }
            }

            return hashObjectArray;
        };

        /*
            Check if a parameter is a possible array
        */

        var isArray = function (possibleArray) {
            if (typeof possibleArray == "undefined"
                || typeof possibleArray.length == "undefined"
                || possibleArray.length == 0) {
                return false;
            }
            return true;
        };

        /*
            Splits a key value string to a typed object

            - keyValueString,   A key value string that will be split into an object with the 'key' and 'value' properties
                                The key value string is in the following format: "key=value"
            
            - valueAsigner      An optional paramter for the character linking the key and the value. If not provided 
                                the default valueASigner will be used
        */

        var getHashObjectFromString = function (keyValueString, valueAsigner) {
            if (typeof keyValueString != "string" || keyValueString == "") {
                return null;
            }

            var asignerCharacter = "";

            if (typeof valueAsigner == "string" && valueAsigner != "") {
                asignerCharacter = valueAsigner;
            }
            else {
                asignerCharacter = ASIGNER;
            }

            var splitArray = keyValueString.split(asignerCharacter);

            // the array must have two elements, the key and the value
            if (splitArray.length != 2) {
                return null;
            } else {
                var keyValueObject = {};

                keyValueObject.key = splitArray[0];
                keyValueObject.value = splitArray[1];

                return keyValueObject;
            }
        };

        /*
            Return a string key value asignment from a hash object
        */

        var getKeyValueStringFromObject = function (object) {
            return object.key + ASIGNER + object.value;
        };

        //#endregion 

        /*
            Revealing module pattern
            ====================================================================================
        */

        //#region RMP

        return {
            // Url hash management functions
            writeHashParameter: writeHashParameter,

            removeHashParameterByKey: removeHashParameterByKey,

            removeHashParameterByKeyValuePair: removeHashParameterByKeyValuePair,

            getHashValueForKey: getHashValueForKey,

            containsKeyValueCombination: containsKeyValueCombination,

            // Global hash parameter functions
            clearAllHashParameters: clearAllHashParameters
        };

        //#endregion

    })();

    // namespace the url query serach module
    DSS.namespace("urlsearch", urlSearchUtility);

})(jQuery);