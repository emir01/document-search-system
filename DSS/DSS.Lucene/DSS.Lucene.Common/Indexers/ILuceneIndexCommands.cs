using Lucene.Net.Index;

namespace DSS.Lucene.Common.Indexers
{
    /// <summary>
    /// Provides low level executions and commands for common lucene index operations.
    /// </summary>
    /// <typeparam name="T">The type of data the index writer is used to index</typeparam>
    public interface ILuceneIndexCommands<in T>
    {
        /// <summary>
        /// Writes a data object to the provided index via the Index writter.
        /// </summary>
        /// <param name="data">The data containing all the information to be stored in the index.</param>
        /// <param name="indexWritter">The index where the data will be stored.</param>
        void AddToIndex(T data, IndexWriter indexWritter);

        /// <summary>
        /// Remove a document fro the index based on a given term.
        /// </summary>
        /// <param name="data">The abstract document representation for the document to be removed from the index</param>
        /// <param name="indexReader">The reader used to remove a document form the index</param>
        void RemoveFromIndex(T data, IndexReader indexReader);

        /// <summary>
        /// Optimizes an index using the given index writer, that should already be setup on the index we want to optimize
        /// </summary>
        /// <param name="indexWriter">The index writer for the index we want to optimize/</param>
        void OptimizeIndex(IndexWriter indexWriter);
    }
}
