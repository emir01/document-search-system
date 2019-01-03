using Lucene.Net.Index;
using Lucene.Net.Store;

namespace DSS.Lucene.Common.IndexReaders
{
    /// <summary>
    /// Constructs the basic Index Reader object.
    /// </summary>
    public class IndexReaderBaseFactory:IIndexReaderFactory<IndexReader>
    {
        /// <summary>
        /// Construct a specific Lucene Index reader.
        /// </summary>
        /// <param name="indexDirectory">The directory where the index is located</param>
        /// <param name="isReadOnly">Flag indicating if the Index readed should be opened in read only mode. Default set to true</param>
        /// <returns>A specific index reader object.</returns>
        public IndexReader GetIndexReader(Directory indexDirectory,bool isReadOnly = true)
        {
            var indexReader = IndexReader.Open(indexDirectory, isReadOnly);
            return indexReader;
        }
    }
}