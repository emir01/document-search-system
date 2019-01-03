
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using DSS.Data.Query.Enums;
using DSS.Data.Query.Filters;
using DSS.Data.Query.QueryProcessObjects;
using DSS.Data.Query.QueryProcessors.Interface;
using DSS.Data.Query.Sorting;

namespace DSS.Data.Query.QueryProcessors
{
    /// <summary>
    /// Describes a grid request processor for a given entitiy.
    /// This is the main object in the Grid Processing functionality.
    /// 
    /// For a given type T it relies on the 
    /// 
    /// The Dynamic Where and Filtering/Sorting functionality is currently handled by dynamic
    /// </summary>
    /// <typeparam name="T">The main entitiy type which is processed</typeparam>
    public class QueryProcessor<T> : IQueryProcessor<T>
    {
        #region Properties

        /// <summary>
        /// Dynamic filter builder.
        /// </summary>
        private readonly IDynamicFilterBuilder<string> _dynamicFilterBuilder;

        #endregion

        #region Constructor

        public QueryProcessor(IDynamicFilterBuilder<string> dynamicFilterBuilder)
        {
            _dynamicFilterBuilder = dynamicFilterBuilder;
        }

        #endregion

        #region Public  Interface Implementation

        /// <summary>
        /// Returns a processed queryable object for a domain entitiy of type T, processed using the Query Parameters object
        /// </summary>
        /// <param name="queryable"> The input queryable that will be transformed and processed based on the Query Parameters </param>
        /// <param name="parameters">The Query parameter object</param>
        /// <returns></returns>
        public QueryResult<T> ProcessQuery(IQueryable<T> queryable, QueryParameters parameters)
        {
            // Get the initial raw count
            int rawCount = 0;
            rawCount = queryable.Count();

            queryable = ProcessSorting(queryable, parameters);

            // Filter stuff first and then add paging
            queryable = ProcessFiltering(queryable, parameters);
            int filteredCount = queryable.Count();

            queryable = ProcessPaging(queryable, parameters);

            // Create the Query Process Result object
            var queryResult = new QueryResult<T> { ProcessedData = queryable, FilteredCount = filteredCount, TotalCount = rawCount };

            return queryResult;
        }


        #endregion

        #region Filter Processing

        /// <summary>
        /// Process the filtering parameters provided via the list of leaf nodes that have been processed
        /// from the scaffolding model
        /// </summary>
        /// <param name="queryable">The queryable collection that contains the query.</param>
        /// <param name="parameters">The Query parameters processed from the client request</param>
        /// <returns></returns>
        private IQueryable<T> ProcessFiltering(IQueryable<T> queryable, QueryParameters parameters)
        {
            // Contains all the dynamic filters
            var dynamicFilters = new List<string>();

            foreach (var filterNode in parameters.Filters)
            {
                if (filterNode is FilterLeafNode)
                {
                    queryable = ProcessSingleFilterLeafNode(queryable, filterNode as FilterLeafNode);
                }
            }

            // return the processed queryable collection that should have
            // the filtering applied
            return queryable;
        }

        #endregion

        #region Filter Leaf Node Processing

        /// <summary>
        ///  Handles all the processing regarding a given leaf filter node for the given queryable.
        /// </summary>
        /// <param name="queryable">The queryable collection that will have a filter applied based on the filter node</param>
        /// <param name="filterLeafNode">The single filter leaf node that will be applied to the queryable</param>
        private IQueryable<T> ProcessSingleFilterLeafNode(IQueryable<T> queryable, FilterLeafNode filterLeafNode)
        {
            // switch on the data type of the filterLeafNode
            switch (filterLeafNode.FilterDataType)
            {
                case FilterDataType.String:
                    queryable = ProcessStringFilterNode(queryable, filterLeafNode);
                    break;
                case FilterDataType.Integer:
                    queryable = ProcessIntegerFilterNode(queryable, filterLeafNode);
                    break;
                case FilterDataType.Decimal:
                    queryable = ProcessDecimalFilterNode(queryable, filterLeafNode);
                    break;
                case FilterDataType.SimpleDropdown:
                    queryable = ProcessSimpleDropdownFilterNode(queryable, filterLeafNode);
                    break;
                case FilterDataType.Date:
                    queryable = ProcessDateFilterNode(queryable, filterLeafNode);
                    break;
                default:
                    queryable = ProcessStringFilterNode(queryable, filterLeafNode);
                    break;
            }

            return queryable;
        }

        /// <summary>
        /// Process a date filter leaf ndoe over the queryable
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="filterLeafNode"></param>
        private IQueryable<T> ProcessDateFilterNode(IQueryable<T> queryable, FilterLeafNode filterLeafNode)
        {
            return queryable;
        }

        /// <summary>
        /// Process a simple dropdown filter leaf node over the queryable.
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="filterLeafNode"></param>
        private IQueryable<T> ProcessSimpleDropdownFilterNode(IQueryable<T> queryable, FilterLeafNode filterLeafNode)
        {
            var dynamicFilter = _dynamicFilterBuilder.GetDropdownDynamicFilter(filterLeafNode);

            // Check if the dynamic filter builder has returned a dynamic filter value
            if (!string.IsNullOrWhiteSpace(dynamicFilter))
            {
                queryable = queryable.Where(dynamicFilter);
            }

            return queryable;
        }

        /// <summary>
        /// Process a decimal filter node over the queryable
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="filterLeafNode"></param>
        private IQueryable<T> ProcessDecimalFilterNode(IQueryable<T> queryable, FilterLeafNode filterLeafNode)
        {
            return queryable;
        }

        /// <summary>
        /// Process an integer filter leaf node over the queryable
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="filterLeafNode"></param>
        private IQueryable<T> ProcessIntegerFilterNode(IQueryable<T> queryable, FilterLeafNode filterLeafNode)
        {
            return queryable;
        }

        /// <summary>
        /// Process and integrate a String based filter leaf node over the queryable collecton
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="filterLeafNode"></param>
        private IQueryable<T> ProcessStringFilterNode(IQueryable<T> queryable, FilterLeafNode filterLeafNode)
        {
            var dynamicFilter = _dynamicFilterBuilder.GetStringDynamicFilter(filterLeafNode);

            if (!string.IsNullOrWhiteSpace(dynamicFilter))
            {
                queryable = queryable.Where(dynamicFilter);
            }

            return queryable;
        }

        #endregion

        #region Paging Processing

        /// <summary>
        /// Process the paging parameters for the given queryable collection based on the Query Parameters object
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IQueryable<T> ProcessPaging(IQueryable<T> queryable, QueryParameters parameters)
        {
            // skip an ammount of entities
            var processedQueryable = queryable.Skip(parameters.Skip);

            // take a numer of parameters
            processedQueryable = processedQueryable.Take(parameters.Take);
            return processedQueryable;
        }

        #endregion

        #region Sorting Processing

        /// <summary>
        /// Process sorting parameters on the queryable collection based on the query parameters sort nodes, created from the
        /// appropriate parameter processors.
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        private static IQueryable<T> ProcessSorting(IQueryable<T> queryable, QueryParameters queryParameters)
        {
            // If there is no sorting nodes set for any reason from the client
            // we will sort on the default initial property
            if (queryParameters.SortNodes.Count == 0)
            {
                // Get the first property name used in initial ordering
                var type = typeof(T);
                var firstPropertyName = type.GetProperties()[0].Name;

                // sort the queryable by the first property of the type  
                queryable = queryable.OrderBy(x => firstPropertyName);

            }
            else
            {
                // Order the sort nodes based on priority. Bassically gives back the same order
                // because of no extra data beeing sent from client for priority.
                var orderedSortNodes = queryParameters.SortNodes.OrderBy(x => x.SortPriority);

                foreach (var orderedSortNode in orderedSortNodes)
                {
                    SortNode node = orderedSortNode;

                    var type = typeof(T);
                    var sortProperty = type.GetProperties().FirstOrDefault(x => x.Name == node.SortTarget);

                    if (sortProperty == null)
                    {
                        // Try and see if there is a property from a processed sort target for rebound Model Properties using the _ splitter
                        var procSortTarget = node.SortTarget.Split('_')[0];

                        sortProperty = type.GetProperties().FirstOrDefault(x => x.Name == procSortTarget);

                        // if we still dont have a sort property we can continue
                        if (sortProperty == null)
                        {
                            continue;
                        }
                    }

                    // Apply the sorting on the target depending on the sort direction
                    if (orderedSortNode.SortDirection == SortDirection.Asc)
                    {
                        queryable = queryable.OrderBy(sortProperty.Name);
                    }
                    else
                    {
                        queryable = queryable.OrderBy(sortProperty.Name + " DESC");
                    }
                }
            }

            return queryable;
        }

        #endregion
    }
}