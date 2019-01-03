using System;
using System.Collections.Generic;
using DSS.Data.Query.Filters;

namespace DSS.Data.Query.FilterScaffolding
{
    /// <summary>
    /// The filter scaffold model is a construct that tracks the current Filter Node list
    /// and can be used as a strongly typed model to construct Web Interfaces
    /// for the scaffolded filtering structure
    /// 
    /// </summary>
    public class FilterScaffoldModel<T>
    {
        #region Filter Node Structure

        /// <summary>
        /// The Filter node scaffold structure is constructed using extensions over the Filter
        /// Scaffold Model.
        ///  </summary>
        public List<FilterNode> FilterNodes { get; set; }

        #endregion

        #region Constructor

        public FilterScaffoldModel()
        {
            FilterNodes = new List<FilterNode>();
        }

        #endregion

        #region Filter Node Structure Accessors

        /*  Todo: Important!!
         *  For now we are going to only use a single level search functionality,
         *  without implementing tree search to prototype the system faster
         */

        /// <summary>
        ///  Returns the node in the node hirearchy with the given node id.
        /// </summary>
        /// <param name="nodeId">The id of the node to search for in the hirearchy</param>
        /// <returns></returns>
        public FilterLeafNode GetLeafNodeById(int nodeId)
        {
            foreach (var filterNode in FilterNodes)
            {
                if (filterNode.NodeId == nodeId && filterNode is FilterLeafNode)
                {
                    return filterNode as FilterLeafNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a node in the node hirearchy with the given node name.
        /// </summary>
        /// <param name="nodeName">The name by which to search in the hirearchy</param>
        /// <returns><see cref="FilterNode"/> object</returns>
        public FilterLeafNode GetLeafNodeByName(string nodeName)
        {
            foreach (var filterNode in FilterNodes)
            {
                if (filterNode.NodeName == nodeName && filterNode is FilterLeafNode)
                {
                    return filterNode as FilterLeafNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a filter root node for a given nodeId
        /// </summary>
        /// <param name="nodeId">The id of the filter root node we are searching for</param>
        /// <returns><see cref="FilterRootNode"/> object with the given id</returns>
        public FilterRootNode GetRootNodeById(int nodeId)
        {
            foreach (var filterNode in FilterNodes)
            {
                if(filterNode is FilterRootNode && filterNode.NodeId == nodeId)
                {
                    return filterNode as FilterRootNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Return a filter root node for a given node name
        /// </summary>
        /// <param name="nodeName">The name of the filter root node we are searching for</param>
        /// <returns><see cref="FilterRootNode"/> object with the given name</returns>
        public FilterRootNode GetRootNodeByName(string nodeName)
        {
            foreach (var filterNode in FilterNodes)
            {
                if (filterNode is FilterRootNode && filterNode.NodeName == nodeName)
                {
                    return filterNode as FilterRootNode;
                }
            }

            return null;
        }

        #endregion

        #region All Leaf Node Accessors

        #endregion
    }
}
