using DSS.Data.Query.Enums;
using DSS.Data.Query.Filters;

namespace DSS.Data.Query.FilterScaffolding
{
    /// <summary>
    /// Contains filter scaffolding extensions over a list of filter scaffold objects.
    /// </summary>
    public static class ScaffoldModelExtensions
    {
        #region Filter Scaffold Model Extensions

        /// <summary>
        /// Add a root node to the scaffold model. The extensions allows the calling code to pass in
        /// Root Nodes with or without an Id or Name and provide the Id and Name implicitly via the extension
        /// function
        /// </summary>
        /// <param name="scaffoldModel">The extended scaffold model</param>
        /// <param name="rootNode">The actual roote node beeing added to the scaffold model</param>
        public static FilterScaffoldModel<T> AddRootNodeToBaseLevel<T>(this FilterScaffoldModel<T> scaffoldModel, FilterRootNode rootNode)
        {
            scaffoldModel.FilterNodes.Add(rootNode);

            return scaffoldModel;
        }

        /// <summary>
        /// Adds a Leaf node to the scaffold model. The Leaf mode is added on the base Filter Scaffold Model level.
        /// The leaf node can either be passed in with a specified node id or node name. The calling code can also 
        /// specifiy implicit node id or node name parameters
        /// </summary>
        /// <param name="scaffoldModel">The scaffold mode beeing extended</param>
        /// <param name="leafNode">The leaf node beeing added to the scaffold model.</param>
        /// <returns></returns>
        public static FilterScaffoldModel<T> AddLeafNodeToBaseLevel<T>(this FilterScaffoldModel<T> scaffoldModel, FilterLeafNode leafNode)
        {
            scaffoldModel.FilterNodes.Add(leafNode);
            return scaffoldModel;
        }

        /// <summary>
        /// Adds a leaf node to the scaffold model  under a given parent node given with the id of the parent node
        /// </summary>
        /// <param name="scaffoldModel">The scaffod model beeiong extended</param>
        /// <param name="leafNode"></param>
        /// <param name="parentNodeId"></param>
        /// <returns></returns>
        public static FilterScaffoldModel<T> AddLeafNodeToParent<T>(this FilterScaffoldModel<T> scaffoldModel, FilterLeafNode leafNode, int parentNodeId)
        {
            var parentNode = scaffoldModel.GetRootNodeById(parentNodeId);
            parentNode.Nodes.Add(leafNode);

            return scaffoldModel;
        }

        /// <summary>
        /// Adds a leaf node under a parent node provided with the given parent node name
        /// </summary>
        /// <param name="scaffoldModel"></param>
        /// <param name="leafNode"></param>
        /// <param name="parentNodeName"></param>
        /// <returns></returns>
        public static FilterScaffoldModel<T> AddLeafNodeToParent<T>(this FilterScaffoldModel<T> scaffoldModel, FilterLeafNode leafNode, string parentNodeName)
        {
            var parentNode = scaffoldModel.GetRootNodeByName(parentNodeName);

            parentNode.Nodes.Add(leafNode);

            return scaffoldModel;
        }

        /// <summary>
        /// Adds a root node to another root node given with the specific parent node id
        /// </summary>
        /// <param name="scaffoldModel">The scaffold model object we are extending</param>
        /// <param name="rootNode">The new root node to be added to the scaffold model hirearchy</param>
        /// <param name="parentNodeId">The internal id of the root node to which we will add the new root node</param>
        /// <returns></returns>
        public static FilterScaffoldModel<T> AddRootNodeToParent<T>(this FilterScaffoldModel<T> scaffoldModel, FilterRootNode rootNode, int parentNodeId)
        {
            var parentNode = scaffoldModel.GetRootNodeById(parentNodeId);

            parentNode.Nodes.Add(rootNode);

            return scaffoldModel;
        }

        /// <summary>
        /// Add a new root node to a given parent node in the scaffold model provided with the parent node name
        /// </summary>
        /// <param name="scaffoldModel"></param>
        /// <param name="rootNode"></param>
        /// <param name="parentNodeName"></param>
        /// <returns></returns>
        public static FilterScaffoldModel<T> AddRootNoodeToParent<T>(this FilterScaffoldModel<T> scaffoldModel, FilterRootNode rootNode, string parentNodeName)
        {
            var parentNode = scaffoldModel.GetRootNodeByName(parentNodeName);

            parentNode.Nodes.Add(rootNode);

            return scaffoldModel;
        }

        #endregion
    }
}
