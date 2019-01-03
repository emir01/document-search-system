/*
    Contains jQuery Datatables Server Communication functionality and utilities
*/

(function ($) {

    var dataTablesServerComm = (function () {

        /*
            Object containing all the callback names for the jQuery Data Tables configuration process
        */

        var callbackNames = {
            FnCookieCallback: "fnCookieCallback",
            FnCreatedRow: "fnCreatedRow",
            FnDrawCallback: "fnDrawCallback",
            FnFooterCallback: "fnFooterCallback",
            FnFormatNumber: "fnFormatNumber",
            FnHeaderCallback: "fnHeaderCallback",
            FnInfoCallback: "fnInfoCallback",
            FnInitComplete: "fnInitComplete",
            FnPreDrawCallback: "fnPreDrawCallback",
            FnRowCallback: "fnRowCallback",
            FnServerData: "fnServerData",
            FnServerParams: "fnServerParams",
            FnStateLoad: "fnStateLoad",
            FnStateLoaded: "fnStateLoaded",
            FnStateLoadParams: "fnStateLoadParams",
            FnStateSave: "fnStateSave",
            FnStateSaveParams: "fnStateSaveParams"
        };

        return {
            CallbackNames: callbackNames
        };
    })();

    /*
        Namespace the jquery data tables extensions
    */

    DSS.namespace("query.tables.objects", dataTablesServerComm);

})(jQuery);