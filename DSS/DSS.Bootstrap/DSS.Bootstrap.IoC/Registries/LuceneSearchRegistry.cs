using DSS.Lucene.Searching.Services;
using DSS.Lucene.Searching.Services.Interfaces;
using StructureMap.Configuration.DSL;

namespace DSS.Bootstrap.IoC.Registries
{
    public class LuceneSearchRegistry:Registry
    {
        public LuceneSearchRegistry()
        {
            For<IIndexSearchingService>().Use<IndexSearchingService>();
        }
    }
}
