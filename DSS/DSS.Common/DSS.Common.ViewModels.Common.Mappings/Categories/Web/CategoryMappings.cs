using AutoMapper;
using DSS.Common.ViewModels.Categories;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Categories.Web
{
    public static class CategoryMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<Category, DisplayCategoryViewModel>();
            Mapper.CreateMap<DisplayCategoryViewModel, Category>();
        }
    }
}
