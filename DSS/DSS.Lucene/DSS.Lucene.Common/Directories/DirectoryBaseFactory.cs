using System.IO;
using Lucene.Net.Store;

namespace DSS.Lucene.Common.Directories
{
    public class DirectoryBaseFactory :IDirectoryFactory<FSDirectory>
    {
        /// <summary>
        /// Creates a Lucene directory of the given type T.
        /// </summary>
        ///<returns>Lucene directory of type T</returns>
        public FSDirectory GetDirectory(string fullDirectoryPath)
        {
            // Create the directory info and open the actual file system directory
            var directoryInfo = new DirectoryInfo(fullDirectoryPath);

            var fileSystemDirectory = FSDirectory.Open(directoryInfo);

            // return the directory
            return fileSystemDirectory;
        }
    }
}
