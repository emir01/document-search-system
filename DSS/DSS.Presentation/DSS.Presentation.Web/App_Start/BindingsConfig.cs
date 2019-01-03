using System.Web.Mvc;
using DSS.Data.Query.CustomBinders;
using DSS.Data.Query.Filters;

namespace DSS.Presentation.Web.App_Start
{
    public class BindingsConfig
    {
        public static void RegisterCustomBinders(ModelBinderDictionary binders)
        {
            AddFilterNodeBindings(binders);
        }

        private static void AddFilterNodeBindings(ModelBinderDictionary binders)
        {
            binders.Add(typeof(FilterNode), new FilterNodeBinder());
        }
    }
}