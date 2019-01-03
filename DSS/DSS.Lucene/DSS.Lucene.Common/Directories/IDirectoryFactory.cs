using Lucene.Net.Store;

namespace DSS.Lucene.Common.Directories
{
    /// <summary>
    /// Basic interface that describes code functionality for creating Lucene Directories.
    /// </summary>
    /// <typeparam name="T">The type of directory the factory is responsible to create.</typeparam>
    public interface IDirectoryFactory<out T> where T:Directory
    {
        /// <summary>
        /// Creates a Lucene directory of the given type T.
        /// </summary>
        ///<returns>Lucene directory of type T</returns>
        T GetDirectory(string fullDirectoryPath);
    }
}
