using Lucene.Net.Index;
using Lucene.Net.Store;

namespace DSS.Lucene.Common.IndexReaders
{
    /// <summary>
    /// A factory used to create index reader objects that can be used to access a lucene index on a given directory.
    /// </summary>
    /// <typeparam name="T">The type of index readed object the factory should be constructing</typeparam>
    public interface IIndexReaderFactory<out T> where T: IndexReader
    {
        /// <summary>
        /// Construct a specific Lucene Index reader.
        /// </summary>
        /// <param name="indexDirectory">The directory where the index is located</param>
        /// <param name="isReadOnly">Flag indicating if the Index readed should be opened in read only mode. Default set to true</param>
        /// <returns>A specific index reader object.</returns>
        T GetIndexReader(Directory indexDirectory, bool isReadOnly = true);
    }
}
