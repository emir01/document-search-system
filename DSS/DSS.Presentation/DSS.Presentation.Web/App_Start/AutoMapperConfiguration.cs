using DSS.Common.ViewModels.Mappings.Categories.Web;
using DSS.Common.ViewModels.Mappings.Documents.Web;
using DSS.Common.ViewModels.Mappings.Keywords.Web;

namespace DSS.Presentation.Web.App_Start
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper()
        {
            DocumentMappings.Map();

            KeywordMappings.Map();

            CategoryMappings.Map();
        }
    }
}