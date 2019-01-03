(function ($) {
    /*
        DataQuery Table Actoin Extensions and functionality, used in constructing and managing Grid Actions.

        Sub module for the query.tables.build namespace. Wraps certain action handling and building functionality.

        The main entry point is the renderRowActions method.
    */

    var dataTableActions = (function () {

        var handlerKeyAttr = "data-handler-key";

        // Setup the general event handler for all the buttons added via the dataTable Action Extensions
        $(function () {
            $(document).on("click", ".data-query-action-button", function (event) {
                var btn = $(this);

                var handlerKey = btn.attr(handlerKeyAttr);

                var handler = getProcessedHandler(handlerKey);

                // Call the handler, by setting the button as the this object and passing the event object
                // as the first parameter
                handler.call(btn[0], event);
            });
        });

        /*
            This collection stores all the function handler references that have been processed by the dataTableActions extensions
            This is used so we can dynamicly call the handlers for buttons set on a grid column using mRender. The actions are
            returned as html and any other event handle processing is not possible
        */

        var processedHandlers = [];

        /*
            Define a action configuration type object, which provides a strongly typed parameter
            storage entitiy for the actions for a given grid

            Params : 
                    - displayText - String | Function, determine the "label" text on the action button
                    - displayTextFlag - Boolean, flag indicating if the dispalyText should be displayed on the button or used as a title only
                    - buttonCssClass -  String, the general custom class string for the action button
                    - buttonIconClasss String | Function, determine the icon class if any to be used on the 
                    - handler Function, the event handler that will be atached to the action
        */

        var actionConfigObject = function (displayText, displayTextFlag, buttonCssClass, buttonIconClass, handler) {
            this.displayText = displayText;

            this.displayTextFlag = displayTextFlag;
            this.buttonCssClass = buttonCssClass;
            this.buttonIconClass = buttonIconClass;

            this.handler = handler;
        };

        /*
            ------------------------------------------------------------------------------------
            Publinc interface
            ------------------------------------------------------------------------------------
        */

        /*
            The data table action extensions callback method that has to be set as the mRender callback
            on the set action column in the tables.build namespace. It should be set insinde a closure
            so the gridConfigurationObject can be passed
            
             Params:
                    -gridConfigurationObject - Set and passed via closures in the setActionColumn function
                                                should contain the configured actions array
                    
                    boundValue, type, dataContext - the regular mRender function parameters passed in via the 
                    closure in setActionColumn method
        */
        var renderRowActions = function (gridConfigurationObject, boundValue, type, dataContext) {

            // go through each of the actions and return an html string 
            var actions = gridConfigurationObject.actions;

            // Create the wrapper div used to wrap around the action elements
            var htmlWrapper = $("<div></div>").addClass("hiddenGridActionConstructArea");

            // For each action construct the html and add it to the htmlWrapper
            for (var kk = 0; kk < actions.length; kk++) {
                var action = actions[kk];

                // Construct the action button HTML
                var button = bootstrapButton();

                // Retrieve the values that can be set via multiple posibilities
                var buttonText = extractValueFromProperty(action.displayText, boundValue, type, dataContext);
                var buttonIcon = extractValueFromProperty(action.buttonIconClass, boundValue, type, dataContext);

                var buttonCssClass = action.buttonCssClass;
                var buttonHandler = action.handler;

                // Set general button/action properties
                button.css(buttonCssClass);
                button.attr("title", buttonText);

                if (action.displayTextFlag) {
                    addButtonText(button, buttonText);
                    addButtonIcon(button, buttonIcon);
                } else {
                    addButtonIcon(button, buttonIcon);
                }

                // Add the class and event handler data used to setup the action handler functionality
                button.addClass("data-query-action-button");

                // check if the handler for the action has been processed 
                var processedHandlerKey = isHandlerProcessed(buttonHandler);
                if (processedHandlerKey >= 0) {
                    // Set the key so we can access the handler in the common click event
                    button.attr(handlerKeyAttr, processedHandlerKey);
                }
                else {
                    // as this handler has not been processed we will add it to the list of 
                    // processed handlers with a new key
                    var key = addProcessedHandler(buttonHandler);
                    button.attr(handlerKeyAttr, key);
                }

                htmlWrapper.append(button);
            }

            // For now return the bound value to the actions column
            var actionsHtml = htmlWrapper.html();

            return actionsHtml;
        };

        /*
          ------------------------------------------------------------------------------------
          Private Function Handler Methods
          ------------------------------------------------------------------------------------
        */

        /*
            Check if the current handler is already processed and referenced in the extensions collection of handlers
        */

        var isHandlerProcessed = function (handler) {
            for (var kk = 0 ; kk < processedHandlers.length; kk++) {
                if (processedHandlers[kk] == handler) {
                    // return the index of the 
                    return kk;
                }
            }

            // Return a negative index
            return -1;
        };

        /*
            Add a processed handler to the collection of handlers.
        */

        var addProcessedHandler = function (handler) {
            processedHandlers.push(handler);

            // return the index/key for the handler
            return processedHandlers.length - 1;
        };

        /*
            Return the handler for the  given key. The key in the current version is just the index of the handler
            in the processed handler array.
        */

        var getProcessedHandler = function (key) {
            var handler = processedHandlers[key];

            if (handler == undefined) {
                return defaultActionHandler;
            }
            else {
                return handler;
            }
        };

        /*
            The Default Handler for the action buttons for a grid. Can be used as a placeholder and to notify
            the client that the handlers have not been correctly set
        */

        var defaultActionHandler = function () {
            alert("This is the default action button handler. Something must have gone wrong");
        };

        /*
          ------------------------------------------------------------------------------------
          Private constructon methods
          ------------------------------------------------------------------------------------
        */

        /*
            Extract the string value from a given property that can be specified either as a raw String or a Function 
            callback that returns a string.

            Params : 
                    - property - The property that is either a string or a function which will be processed
                    - boundValue, type, dataContext - The mDraw Callback parameters that can be used b callback functions
                                                        to determine the text value

        */

        var extractValueFromProperty = function (property, boundValue, type, dataContext) {

            var value = "";

            // if the property is a function , invo
            if (typeof property == "function") {

                // invoke the function passing back the mRender parameters
                value = property(boundValue, type, dataContext);
            }
            else if (typeof property == "string") {
                value = property;
            }

            return value;
        };

        /*
                Create a simple bootstrap button wrapped in a jquery object
        */

        var bootstrapButton = function () {
            var btn = $("<button></button>");
            btn.addClass("btn btn-small");
            return btn;
        };

        /*
            Add an icon element with the specified icon class to the button
        */

        var addButtonIcon = function (button, iconClass) {
            if (iconClass != "") {
                var iconWrapper = $("<i></i>").addClass(iconClass);
                button.append(iconWrapper);
            }
        };

        /*
            Add a text wraped in a span element to the button.
        */
        var addButtonText = function (button, text) {
            var textWrapper = $("<span></span>").text(text);
            button.append(textWrapper);
        };

        return {
            // Objects
            ActionConfigObject: actionConfigObject,

            // Public Interface
            RenderRowActions: renderRowActions
        };
    })();

    /*
        Namespace the jquery data tables actions extensions
    */

    DSS.namespace("query.tables.actions", dataTableActions);

})(jQuery);