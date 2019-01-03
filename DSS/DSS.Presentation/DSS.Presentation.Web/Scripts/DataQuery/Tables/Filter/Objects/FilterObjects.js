/*
    Client side Filter Leaf Node and assosiated object representations
*/

(function ($) {

    var dataQueryFilterObject = (function () {

        /*
            Attributre names for the values stored on the  filter input elements
        */

        var filterAttributeNames = {
            BooleanOperator: "data-filter-operator",
            
            OperationType: "data-filter-operation-type",
            
            CaseSensitivity: "data-filter-case",
            
            FilterDataType: "data-filter-data-type",
            
            DropdownValueType: "data-dropdown-value-type",
            
            Target: "data-filter-target",
        };

        /*
            The js Boolean Operator Enum
        */

        var booleanOperator = {
            And: 0,
            Or: 1
        };

        /*
            The js Operation Type enum
        */

        var filterOperationType = {
            GreaterThan: 0,
            GreaterThanOrEqual: 1,

            LessThan: 2,
            LessThanOrEqual: 3,

            Equal: 4,

            Contains: 5
        };

        /*
            The javascript CaseSensitivy Enum
        */

        var filterCaseSensitivity = {
            CaseSensitive: 0,
            CaseInsensitive: 1
        };

        /*
            The javascript Filter Data Type Enum
        */

        var filterDataType = {
            // Filter String values
            String: 0,

            // Filter any whore number value
            Integer: 1,

            // Filter decimal values
            Decimal: 2,

            // Filter reference id values, FKs
            SimpleDropdown: 3,

            // Filter a date value
            Date: 4
        };

        /*
            The javascript counterpart of the Dropdown Value Data Type server object
            used to designate the common value types used in dropdown values
        */

        var dropdownValueTypes = {
            // The value in the dropdowns is a string value and it should be filter processed as such.
            String: 0,

            // The value is an integer value and it should be processed as such
            Integer: 1,

            // The value is a GUID value and it should be processed as such. Reference other entities
            Guid: 2,
            
            // The value for the dropdown are booleans and they should be processed as such
            Boolean:3
        };

        /*
            The javascript filter leaf noder representation
        */

        var filterLeafNode = function (value, target, operationType, caseSensitivity, dataType, operator, dropdownValueType) {
            this.value = value || "";
            this.target = target || "";

            this.operationType = operationType || filterOperationType.Equal;

            this.caseSensitivity = caseSensitivity || filterCaseSensitivity.CaseSensitive;

            this.filterDataType = dataType || filterDataType.String;
            this.dropdownValueType = dropdownValueType || dropdownValueTypes.Integer;
            
            this.operator = operator || booleanOperator.And;
        };

        /*
            Expose the client side objects
        */
        return {
            //The Filter attribute names on the input elements for fitler props
            FilterAttributeNames: filterAttributeNames,

            // The leaf node object
            FilterLeafNode: filterLeafNode,

            // The enum objects
            BooleanOperator: booleanOperator,

            DropdownValueTypes: dropdownValueTypes,

            FilterOperationType: filterOperationType,

            FilterCaseSensitivity: filterCaseSensitivity,

            FilterDataType: filterDataType
        };
    })();

    /*
        Namespace the jquery data tables extensions
    */

    DSS.namespace("query.tables.filterObjects", dataQueryFilterObject);

})(jQuery);