using System;
using System.Collections.Generic;
using DSS.Data.Query.DataTables;
using DSS.Data.Query.Enums;
using DSS.Data.Query.FilterScaffolding;
using DSS.Data.Query.Filters;
using DSS.Data.Query.ParameterProcessors.Interfaces;
using DSS.Data.Query.QueryProcessObjects;
using DSS.Data.Query.Sorting;

namespace DSS.Data.Query.ParameterProcessors
{
    /// <summary>
    /// A Query paramater processor object for the JQueryDataTableParams functionality.
    /// </summary>
    /// <typeparam name="T">The general query parameter object type</typeparam>
    /// <typeparam name="TE">The actual type of the Entitiy for which the query is beeing processed</typeparam>
    public class JQueryDtParameterProcessor<T, TE> : IQueryParameterProcessor<T, TE> where T : JQueryDataTableParams
    {

        #region Public Methods
        /// <summary>
        /// Transforms a raw parameters objects for basic query params of type T as well as optional 
        /// Filter Scaffold Filter params of type TK to the general used Query Parameters object
        ///  </summary>
        /// <param name="generalQueryParameters"> The General Query Parameters describing general query properties( paging, sorting)</param>
        /// <param name="filterScaffoldModel">The Filter Scaffold model providing the filtering information for the Query Paramters object </param>
        /// <returns><see cref="QueryParameters"/> object that can be used by the main query processor</returns>
        public QueryParameters ProcessParameters(T generalQueryParameters, FilterScaffoldModel<TE> filterScaffoldModel = null)
        {
            // Cast the general parameters object
            var castGeneralQueryParameters = generalQueryParameters as JQueryDataTableParams;

            var processedQueryParameters = new QueryParameters();

            ProcessPagingParams(processedQueryParameters, castGeneralQueryParameters);

            ProcessSortingParams(processedQueryParameters, castGeneralQueryParameters);

            ProcessFilteringParams(processedQueryParameters, filterScaffoldModel);

            return processedQueryParameters;
        }

        #endregion

        #region Paging Parameter Processing

        /// <summary>
        /// Sets the paging parameters, Skip and Take on the QueryParameters Common object  from the processed/ing JQueryDAtaTableParams object.
        /// </summary>
        /// <param name="processedParameters"></param>
        /// <param name="generalQueryParameters"></param>
        private void ProcessPagingParams(QueryParameters processedParameters, JQueryDataTableParams generalQueryParameters)
        {
            processedParameters.Skip = generalQueryParameters.iDisplayStart;
            processedParameters.Take = generalQueryParameters.iDisplayLength;
        }

        #endregion

        #region Sorting Parameter Processing

        /// <summary>
        /// Process the sorting parameters and create the array of sort nodes that will be used by query processors
        /// </summary>
        /// <param name="processedQueryParameters"></param>
        /// <param name="castGeneralQueryParameters"></param>
        private void ProcessSortingParams(QueryParameters processedQueryParameters, JQueryDataTableParams castGeneralQueryParameters)
        {
            // Reference all the bound property names as sent from the client
            var boundDataProperties = castGeneralQueryParameters.amDataProp;

            var sortPriority = 0;

            // Reference the sorting directions array
            var sortingDirections = castGeneralQueryParameters.asSortDir;

            foreach (var columBeingSorted in castGeneralQueryParameters.aiSortCol)
            {
                var sortNode = new SortNode
                                   {
                                       SortTarget = boundDataProperties[columBeingSorted],
                                       SortPriority = sortPriority++,
                                       SortDirection =
                                           GetSortDirection(sortingDirections, columBeingSorted) == "asc"
                                               ? SortDirection.Asc
                                               : SortDirection.Desc
                                   };

                // Set the sorting target
                processedQueryParameters.SortNodes.Add(sortNode);
            }
        }

        /// <summary>
        /// Check and return the propper sorting direction for the column beeing sorted
        /// </summary>
        /// <param name="sortingDirections"></param>
        /// <param name="columBeingSorted"></param>
        /// <returns></returns>
        private string GetSortDirection(List<string> sortingDirections, int columBeingSorted)
        {
            // if multiple sort return the appropriate sorting direction 
            if (sortingDirections.Count > columBeingSorted)
            {
                return sortingDirections[columBeingSorted];
            }

            // if there is at least one sorting direction 
            // return the initial sort direction
            if (sortingDirections.Count > 0)
            {
                return sortingDirections[0];
            }

            // return the default sorting direction valuer value
            return "asc";
        }

        #endregion

        #region Filtering Scaffold Processing

        /// <summary>
        /// Process filtering parameters from the FilterScaffoldModel if provided to the general Query Parameters object 
        /// </summary>
        /// <param name="processedQueryParameters"></param>
        /// <param name="filterScaffoldModel"></param>
        private void ProcessFilteringParams(QueryParameters processedQueryParameters, FilterScaffoldModel<TE> filterScaffoldModel)
        {
            // If the filter scaffold model has not been provided we are going to ignore and return
            if (filterScaffoldModel == null)
            {
                return;
            }

            // we are also checking the filter node collection on the filter scaffold model
            if (filterScaffoldModel.FilterNodes == null || filterScaffoldModel.FilterNodes.Count == 0)
            {
                return;
            }

            // if everything passes go and process each filter node individually

            foreach (var scaffoldFilterNode in filterScaffoldModel.FilterNodes)
            {
                // only add the filter node if it is a root node that has at least one leaf node
                // or it is a leaf node that has  a set value

                if (scaffoldFilterNode is FilterLeafNode)
                {
                    // Check if the leaf node value is set to anything and is not a default value
                    var nodeAsLeaf = scaffoldFilterNode as FilterLeafNode;

                    // check if the value for the leaf node is not null
                    // for object values we are hardly going to get an strongly typed value type so we can ignore value type checks
                    if (nodeAsLeaf.Value != null)
                    {
                        processedQueryParameters.Filters.Add(scaffoldFilterNode);
                    }

                }
                else
                {
                    // Scaffold Filte Node is a Root Node
                    var nodeAsRoot = scaffoldFilterNode as FilterRootNode;

                    if (nodeAsRoot != null && (nodeAsRoot.Nodes != null && nodeAsRoot.Nodes.Count > 0))
                    {
                        // add the root node
                        processedQueryParameters.Filters.Add(scaffoldFilterNode);        
                    }
                }
            }
        }

        #endregion
    }
}