using Lucene.Net.Index;
using Lucene.Net.Search;

namespace DSS.Lucene.Common.IndexSearchers
{
    /// <summary>
    /// Basic interface describing functionality for creating index searchers.
    /// </summary>
    public interface IIndexSearcherFactory<out T> where T:IndexSearcher
    {
        T GetIndexSearcher(IndexReader reader);
    }
}
