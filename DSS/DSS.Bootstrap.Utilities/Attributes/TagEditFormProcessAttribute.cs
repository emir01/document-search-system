using System;
using System.Linq;
using System.Web.Mvc;

namespace DSS.Bootstrap.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TagEditFormProcessAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // we are just going to add to the forms in the context any kley values that end at []
            var tagEditKeys = filterContext.HttpContext.Request.Form.AllKeys.Where(x => x.LastIndexOf("[]", System.StringComparison.Ordinal) == x.Length - 2);

            foreach (var tagEditKey in tagEditKeys)
            {
                var newKey = tagEditKey.Substring(0, tagEditKey.Length - 2);
                var value = filterContext.HttpContext.Request.Form[tagEditKey];

                filterContext.RequestContext.HttpContext.Items.Add(newKey, value);
            }
        }
    }
}