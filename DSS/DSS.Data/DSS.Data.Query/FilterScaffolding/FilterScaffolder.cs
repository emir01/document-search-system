namespace DSS.Data.Query.FilterScaffolding
{
    /// <summary>
    /// A filter scaffolder that can be used to configure a scaffolding
    /// structure that can be then used to construct user interfaces 
    /// for interacting withthe DSS.Data.Query filtering structure 
    /// for a given entitiy.
    ///  </summary>
    public class FilterScaffolder
    {
        /// <summary>
        /// Return a filter scaffold model object with properties mapped from the Entntiy
        /// that the filter scaffolder is based on
        /// </summary>
        /// <returns></returns>
        public static FilterScaffoldModel<T> GetFilterScaffoldModel<T>()
        {
            var filterScaffold = new FilterScaffoldModel<T>();

            return filterScaffold;
        }
    }
}
