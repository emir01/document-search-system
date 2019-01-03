using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Common.Services;
using StructureMap.Configuration.DSL;

namespace DSS.Bootstrap.IoC.Registries
{
    public class BlWebRegistry:Registry
    {
        public BlWebRegistry()
        {
            For<IDocumentsService>().Use<DocumentsService>();

            For<IDocumentSearchService>().Use<DocumentSearchService>();

            For<IKeywordService>().Use<KeywordService>();

            For<ICategoryService>().Use<CategoryService>();

            For<IDssIndexService>().Use<DssIndexService>();

            For<IUserService>().Use<UserService>();
        }
    }
}
