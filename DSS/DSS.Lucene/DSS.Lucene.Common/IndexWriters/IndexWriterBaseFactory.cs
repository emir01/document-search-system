using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace DSS.Lucene.Common.IndexWriters
{
    public class IndexWriterBaseFactory:IIndexWriterFactory
    {
        public IndexWriter BuildIndexWritter(Directory directory, Analyzer analyzer)
        {
            var indexWriter = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.LIMITED);
            return indexWriter;
        }
    }
}


