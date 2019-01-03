using System;
using System.Web.Mvc;
using DSS.Data.Query.Filters;

namespace DSS.Data.Query.CustomBinders
{
    /// <summary>
    /// Model Binder used to bind Filter Nodes in the Filter Scaffold modef stack functionality.
    /// Used to avoid the Abstract class binding issues when we are using a FilterModelScaffold parameter in controller actions
    /// </summary>
    public class FilterNodeBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // Only leaf nodes have Valuie property
            var valueKey = bindingContext.ModelName + ".Value";
            var valueProperty = bindingContext.ValueProvider.GetValue(valueKey);

            if (valueProperty != null)
            {
                var model = new FilterLeafNode();
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(FilterLeafNode));
                return model;
            }
            else
            {
                var model = new FilterRootNode();
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(FilterRootNode));
                return model;     
            }
        }
    }
}
