using Lucene.Net.Index;
using Lucene.Net.Search;

namespace DSS.Lucene.Common.IndexSearchers
{
    public class IndexSearcherBaseFactory:IIndexSearcherFactory<IndexSearcher>
    {
        public IndexSearcher GetIndexSearcher(IndexReader reader)
        {
            var indexSearcher = new IndexSearcher(reader);
            return indexSearcher;
        }
    }
}
