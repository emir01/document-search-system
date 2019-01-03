using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using DSS.Common.ViewModels.Documents;

namespace DSS.Presentation.Web.CustomBinders
{
    public class TagEditPostedFormBinder:DefaultModelBinder
    {
        /// <summary>
        /// Binds the model by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// The bound object.
        /// </returns>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param><param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param><exception cref="T:System.ArgumentNullException">The <paramref name="bindingContext "/>parameter is null.</exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var bound = base.BindModel(controllerContext, bindingContext);

            var documentsCollection = bound as List<AddDocumentViewModel>;

            // if its null just return the automaticly bound value
            if (documentsCollection == null)
            {
                return bound;
            }
            
            for (int i = 0; i < documentsCollection.Count; i++)
            {
                var valueForKey = bindingContext.ValueProvider.GetValue("documents[" + i + "].KeywordsList[]");

                // if there is no value for the key
                if (valueForKey == null)
                {
                    continue;
                }

                var addDocumentViewModel = documentsCollection[i];

                // set the keyword list

                if (!string.IsNullOrWhiteSpace(valueForKey.AttemptedValue))
                {
                    addDocumentViewModel.KeywordsList = valueForKey.AttemptedValue.Split(',').ToList();    
                }
            }

            return documentsCollection;
        }
    }
}