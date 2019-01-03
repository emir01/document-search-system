using System;
using System.Collections.Generic;
using System.Linq;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Services
{
    public class CategoryService : ICategoryService
    {
        #region Properties

        private readonly IRepository<Category> _categoryRepository;

        #endregion

        #region Constructor

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Returns a list of all the categories wrapped in a data result object.
        /// </summary>
        /// <returns><see cref="DataResult{T}"/> object wrapping the list of categories</returns>
        public DataResult<IList<Category>> GetAllCategories()
        {
            var result = new DataResult<IList<Category>>();
            
            try
            {
                // get all the categories from the repository
                var allCategories = _categoryRepository.ReadAll().ToList();

                result.SetSuccess("Success in retrieving all the categories");
                result.SetData(allCategories);
            }
            catch(Exception ex)
            {
                result.SetException(ex,"Exception when retrieving all categories");
            }

            return result;
        }

        #endregion
    }
}
