/*
    This module is reposnible for performing the search functionality
    for the Document Serach View Module.

    Serach functionality will be performed with the following functionalities in mind:
    
        - Be able to search indexed document content using lucene

        - Be able to select any number of categories, returning documents that 
            belong to the combination of categories

        - Be able to select any number of keywords returning documents that 
            contain the combination of keywords 

        - Any combination of the above

        - The serach query should be composed in the url using a query string.
        
        - The module should be able to process http links containin search query strings
            and display the appropriate results on page load. Allowing linking of search parameters
            between users and application pages


    Relies heavily on the Query String Utility for manipulating the url and query string
*/

(function ($) {

    var searchQueryModule = (function () {

        /*
            Query Configuration, parameters and related services
            ==============================================================================
        */


        // The collection of hash keys used to store and access search parameters in the url

        var categoryKey = "category";

        var keywordKey = "keyword";

        var contentKey = "content";

        var titleKey = "title";

        var authorKey = "author";
        
        /*
            The short reference to the url search utility used to write hash parameters
        */
        
        var urlSearch = DSS.urlsearch;

        /*
            An ajax repository used to query server side dta.
        */

        var documentRepository;

        /*
            Reference to the documents observable array
        */

        var documentObsReference;

        /*
            Initialization
            ==============================================================================

            // Gets passed a reference to the documents observable collection for future referencing
            // and in
        */

        var initQueryModule = function (documentsObservable) {
            // init the document repository on the Search controller
            documentRepository = new DSS.Common.Framework.BaseRepository("document", "", "search");

            documentObsReference = documentsObservable;
        };

        /*
            The number of results returned on each trip to the server 
        */
        var numberOfResultsPerQuery = 10;

        /*
            General Query functionality.
            ==============================================================================
            The documents on the search page will be queried by processing the query string
            which will contain the selected filters on multiple criteria as described above
        */

        /*
            General query on the server for documents matching the query criteria
            The query returns a fixed number of initial results for the query

            Always resets the collection of documents as it usually means a new criteria has been
            added or the page is reloaded.

            If we want to continue populating the documents array fetchNext should be used.
        */

        var doQuery = function (documentsObservable, highLevelCallback, queryFromInternalCriteriaCall) {

            // Process Query String

            var categories = urlSearch.getHashValueForKey(categoryKey);

            var keywords = urlSearch.getHashValueForKey(keywordKey);

            var content = urlSearch.getHashValueForKey(contentKey);

            var title = urlSearch.getHashValueForKey(titleKey);

            var author = urlSearch.getHashValueForKey(authorKey);

            // Create the search object
            var searchObject = {
                Skip: 0,
                Take: numberOfResultsPerQuery,
                Categories: categories,
                Keywords: keywords,
                Content: content,
                Title: title,
                Author: author
            };

            // Get the server results 

            // For now get only the regular data as it is done in
            // the document search view model

            documentRepository.QueryAll(
                { searchFilterModel: searchObject },
                function (data, status, object) {
                    
                    doQueryCallSuccess(documentsObservable, highLevelCallback, data, status, object);
                    
                },

                function (object, status, thrown) {
                    
                    doQueryCallError(documentsObservable, highLevelCallback, object, status, thrown);
                    
                },
                "Search");
        };
        
        /*
            Clear the current criteria and redo the search on the default search configuration.
            This would re-get the initial number of default page size documents possibly sorted in a appropriate    order (date uploaded)

            Params:

                documentsObservable - The observable array containing the documents

        */

        var clearAllCriteria = function (documentsObservable, highLevelCallback) {
            // Clear all the query parameters and then 
            urlSearch.clearAllHashParameters();
        };
        
        /*
            Infinite scrolling functionality implementation.

            Fetches the next numberOfResultsPerQuery using the pre-selected criteria.
        */

        var fetchNext = function (documentsObservable) {
            DSS.msg.info("Fetching next data");
        };

        /*
            Query By Category Functionality
            ==============================================================================
        */

        /*
            Add a category query parameter, updating the documents observable with the new results,
            documents that contain the new category.

            Params:
                documentsObservable -   The observable array that will be updated with the new results,
                                        after querying with the new category

                categoryName        -   The name of the category that we are adding to the query

                categoryId          -   An optional specif value for 

                highLevelCallback   -   An optional argument containing the higher level callback after the documents have finished loading

        */

        var extendQueryForCategory = function (documentsObservable, categoryName, highLevelCallback) {
            
            DSS.urlsearch.writeHashParameter(categoryKey, categoryName, false);

            if (typeof highLevelCallback == "function") {
                highLevelCallback(categoryName);
            }
        };
        
        /*
         * Remove a single category from the url
         */
        
        var removeCategoryFromQuery = function (documentsObservable, categoryName, highLevelCallback) {
            DSS.urlsearch.removeHashParameterByKeyValuePair(categoryKey, categoryName, false);
            
            if (typeof highLevelCallback == "function") {
                highLevelCallback(categoryName);
            }
        };

        /*
         * Removes all category related hash parameters in the url string
         */
        var removeAllCategoriesFromQuery = function() {
            DSS.urlsearch.removeHashParameterByKey(categoryKey, false);
        };

        /*
            Query By Keyword Functionality
            ==============================================================================
        */

        /*
            Extend the documents query by adding a keyword to the criteria. After the keyword is added
            to the query the documents observable should be repopulated
        */

        var extendQueryForKeyword = function (documentsObservable, keywordName, highLevelCallback) {
            DSS.urlsearch.writeHashParameter(keywordKey, keywordName, false);

            if (typeof highLevelCallback == "function" && highLevelCallback != null) {
                highLevelCallback(keywordName);
            }
        };

        /*
            Removes a keyword from the current documents query criteria. After removing the query
            the documents observable is refreshed
        */

        var removeKeywordFromQuery = function (documentsObservable, keywordName) {

        };

        /*
            Query By Content Functionality
            ==============================================================================
        */

        /*
            Extend the stored hash query with the content criteria
        */

        var extendQueryForContent = function (documentsObservable, content, highLevelCallback) {
            DSS.urlsearch.writeHashParameter(contentKey, content, true);

            if (typeof highLevelCallback == "function" && highLevelCallback != null) {
                highLevelCallback(content);
            }
        };

        /*
            Removes the content criteria from the query
        */

        var removeContentFromQuery = function (documentsObservable) {
            DSS.urlsearch.removeHashParameterByKey(contentKey);
        };

        /*
            Query By Title Functionality
            ==============================================================================
        */

        /*
            Extend the stored hash query with the title criteria
        */

        var extendQueryForTitle = function (documentsObservable, title, highLevelCallback) {
            DSS.urlsearch.writeHashParameter(titleKey, title, true);

            if (typeof highLevelCallback == "function" && highLevelCallback != null) {
                highLevelCallback(title);
            }
        };

        /*
            Removes the title criteria from the query
        */

        var removeTitleFromQuery = function (documentsObservable) {
            DSS.urlsearch.removeHashParameterByKey(titleKey);
        };

        /*
            Query By Author Functionality
            ==============================================================================
        */

        /*
          Extend the stored hash query with the author criteria
        */

        var extendQueryForAuthor = function (documentsObservable, author, highLevelCallback) {
            DSS.urlsearch.writeHashParameter(authorKey, author, true);

            if (typeof highLevelCallback == "function" && highLevelCallback != null) {
                highLevelCallback(author);
            }
        };

        /*
            Removes the author criteria from the query
        */

        var removeAuthorFromQuery = function (documentsObservable) {
            DSS.urlsearch.removeHashParameterByKey(authorKey);
        };

        /*
            Internal methods and callbacks
            ==============================================================================
        */

        /*
            Ajax success callback proxy used to populate the documentsObservable on a success
            query to the server
        */

        var doQueryCallSuccess = function (documentsObservable, highLevelCallback, data, status, object) {
            DSS.json.process(data, function (processedData) {
                if (processedData != null) {
                    documentsObservable(processedData);
                } else {
                    documentsObservable([]);
                }
                
                highLevelCallback(processedData, true, object);
            });
        };

        /*
            The error callback for the doQuery functionality
        */

        var doQueryCallError = function (documentsObservable, highLevelCallback, object, status, thrown) {
            documentsObservable([]);
            highLevelCallback([], false, object);
        };

        /*
            Revealing module pattern
            ==============================================================================
        */

        return {
            // Initialization
            Init: initQueryModule,

            // general Query functionality
            doQuery: doQuery,
            fetchNext: fetchNext,
            clearAllCriteria:clearAllCriteria,
            
            // query by category 
            extendQueryForCategory: extendQueryForCategory,
            removeCategoryFromQuery: removeCategoryFromQuery,
            removeAllCategoriesFromQuery: removeAllCategoriesFromQuery,

            // query by keyword
            extendQueryForKeyword: extendQueryForKeyword,
            removeKeywordFromQuery: removeKeywordFromQuery,

            // query by content
            extendQueryForContent: extendQueryForContent,
            removeContentFromQuery: removeContentFromQuery,

            // query by title
            extendQueryForTitle: extendQueryForTitle,
            removeTitleFromQuery: removeTitleFromQuery,

            // query by author
            extendQueryForAuthor: extendQueryForAuthor,
            removeAuthorFromQuery: removeAuthorFromQuery
        };
    })();

    // Namespace the module under the DSS namespace
    DSS.namespace("Ko.Utilities.SearchQueryModule", searchQueryModule);

})(jQuery);