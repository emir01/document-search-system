using DSS.Lucene.Common.Entities;
using DSS.Lucene.Indexing.Services;
using DSS.Lucene.Indexing.Services.Interface;
using DSS.Lucene.Tika;
using DSS.Lucene.Tika.Interface;
using StructureMap.Configuration.DSL;

namespace DSS.Bootstrap.IoC.Registries
{
    public class LuceneIndexingRegistry : Registry
    {
        public LuceneIndexingRegistry()
        {
            For<ITextExtractor>().Use<TextExtractor>();
            For<ILuceneIndexService<IndexedDocument>>().Use<IndexedDocumentIndexer>();
        }
    }
}