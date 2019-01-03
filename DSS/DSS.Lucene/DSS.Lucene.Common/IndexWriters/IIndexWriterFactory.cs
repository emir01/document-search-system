using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace DSS.Lucene.Common.IndexWriters
{
    /// <summary>
    /// The interface that describes functionality to create Lucene Index Writers.
    /// </summary>
    public  interface IIndexWriterFactory
    {
        /// <summary>
        /// Builds and index writer based on the given index directory and analyzer
        /// </summary>
        /// <param name="directory">The directory where the index is located.</param>
        /// <param name="analyzer">The analyzer used to create the IndexWriter</param>
        /// <returns><see cref="IndexWriter"/> object based in the given directory using the given analyzer</returns>
        IndexWriter BuildIndexWritter(Directory directory, Analyzer analyzer);
    }
}
