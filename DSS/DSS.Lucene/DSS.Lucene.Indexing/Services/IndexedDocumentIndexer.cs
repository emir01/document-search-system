using System;
using DSS.Lucene.Common.Analyzers;
using DSS.Lucene.Common.Directories;
using DSS.Lucene.Common.Entities;
using DSS.Lucene.Common.Entities.Enums;
using DSS.Lucene.Common.IndexReaders;
using DSS.Lucene.Common.IndexWriters;
using DSS.Lucene.Common.Indexers;
using DSS.Lucene.Indexing.Services.Interface;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace DSS.Lucene.Indexing.Services
{
    public class IndexedDocumentIndexer : ILuceneIndexService<IndexedDocument>
    {
        #region Properties

        #region Factories

        private IDirectoryFactory<Directory> _directoryFactory;

        private IAnalyzerFactory<Analyzer> _analyzerFactory;

        private readonly IIndexWriterFactory _indexWriterFactory;

        private readonly IIndexReaderFactory<IndexReader> _indexReaderFactory;

        private readonly ILuceneIndexCommands<IndexedDocument> _indexCommands;

        #endregion

        #region Constructor

        public IndexedDocumentIndexer()
        {
            _indexReaderFactory = new IndexReaderBaseFactory();
            _directoryFactory = new DirectoryBaseFactory();
            _analyzerFactory = new AnalyzerBaseFactory();
            _indexWriterFactory = new IndexWriterBaseFactory();
            _indexCommands = new IndexedDocumentCommands();
        }

        #endregion

        #endregion

        /// <summary>
        /// Adds a data object of type T to the index
        /// </summary>
        /// <param name="indexDirectoryPath">The directoryFactory path where the index is located.</param>
        /// <param name="data">The data object that will be stored in the index</param>
        /// <returns>Boolean value wrapped result indicating the success of the operation.</returns>
        public IndexQueryResult<bool> AddToIndex(string indexDirectoryPath, IndexedDocument data)
        {

            var result = new IndexQueryResult<bool>();
            try
            {
                var indexWriter = BuildIndexWriter(indexDirectoryPath);

                // use the indexer to write the data to the index
                _indexCommands.AddToIndex(data, indexWriter);

                indexWriter.Dispose();

                result.SetSuccess("Success in writing document in index");
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result;
        }

        /// <summary>
        /// Remove the object described with the data ttribute from the Lucene Index.
        /// </summary>
        /// <param name="indexDirectoryPath">The path to the Lucene Index directory</param>
        /// <param name="data">The data representing the object to be removed from the index</param>
        /// <param name="optimzeOnRemoval">Boolean flag indicating if we want to optimze the index on removal</param>
        /// <returns>Boolean value indicating the success of the operation</returns>
        public IndexQueryResult<bool> RemoveFromIndex(string indexDirectoryPath, IndexedDocument data, bool optimzeOnRemoval)
        {
            var result = new IndexQueryResult<bool>();
            try
            {
                var reader = BuildIndexReader(indexDirectoryPath);

                _indexCommands.RemoveFromIndex(data, reader);

                reader.Dispose();

                if (optimzeOnRemoval)
                {
                    var optimizeResult = OptimizeIndex(indexDirectoryPath);

                    if (optimizeResult.GetStatus() == LuceneIndexingStatus.Success)
                    {
                        result.SetSuccess("Document was removed from index and index was optimized");
                    }
                    else
                    {
                        result.SetFailiure("Document removed but failed to optimize index.");
                    }
                }
                else
                {
                    result.SetSuccess();
                }
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result;
        }

        /// <summary>
        /// Call the raw optimze command on lower level index objects.
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index we will be optimizing</param>
        /// <param name="options"> Optional Index optimization parameters object. If null default options are used </param>
        /// <returns>Boolean value indicating the success of the operation</returns>
        public IndexQueryResult<bool> OptimizeIndex(string indexDirectoryPath, IndexOptimizeOptions options = null)
        {
            var result = new IndexQueryResult<bool>();
            try
            {
                var writer = BuildIndexWriter(indexDirectoryPath);

                // if options are not provided just optimize with default setting
                if (options == null || (!options.DoWait.HasValue && !options.MaxNumberOfSegments.HasValue))
                {
                    writer.Optimize();
                }
                else
                {
                    if (options.DoWait.HasValue && options.MaxNumberOfSegments.HasValue)
                    {
                        writer.Optimize(options.MaxNumberOfSegments.Value, options.DoWait.Value);
                    }
                    else if (options.DoWait.HasValue)
                    {
                        writer.Optimize(options.DoWait.Value);
                    }
                    else
                    {
                        writer.Optimize(options.MaxNumberOfSegments.Value);
                    }
                }
                
                writer.Dispose();
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result;
        }

        /// <summary>
        /// Returns a boolean flag indicating if the index is optimized.
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index for which we will be checking optimization flags</param>
        /// <returns>Boolean value indicating if the index is optimzed</returns>
        public IndexQueryResult<bool> IsIndexedOptimized(string indexDirectoryPath)
        {
            var result = new IndexQueryResult<bool>();

            try
            {
                var reader = BuildIndexReader(indexDirectoryPath);
                var isOptimized = reader.IsOptimized();

                result.SetSuccess("Successfully retrieved optimized state", isOptimized);
                
                // dispose of the index reader
                reader.Dispose();
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result;
        }

        /// <summary>
        /// Returns the number of documents removed from the index
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index for which we will be checking number of deleted documents</param>
        /// <returns>Integer value indicating the number of deleted documents</returns>
        public IndexQueryResult<int> GetDeletedDocuments(string indexDirectoryPath)
        {
            var result = new IndexQueryResult<int>();

            try
            {
                var reader = BuildIndexReader(indexDirectoryPath);

                var deletedDocsNumber = reader.NumDeletedDocs;
                result.SetSuccess("Successfully retrieved deleted documents number", deletedDocsNumber);

                reader.Dispose();
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result;
        }

        /// <summary>
        /// Get the total number of active documents in the index
        /// </summary>
        /// <param name="indexDirectoryPath"> The directory of the index for which we will be checking number of total indexed documents</param>
        /// <returns></returns>
        public IndexQueryResult<int> GetDocumentsNumber(string indexDirectoryPath)
        {
            var result = new IndexQueryResult<int>();

            try
            {
                var reader = BuildIndexReader(indexDirectoryPath);
                var numberIndexedDocuments = reader.NumDocs();

                result.SetSuccess("Successfully retrieved number of indexe documents",numberIndexedDocuments);

                reader.Dispose();
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result;
        }

        #region Private Builders

        /// <summary>
        /// Build a basic index writer for the index located at the given index path.
        /// </summary>
        /// <param name="indexDirectoryPath">The directory of the index.</param>
        /// <returns>Default <see cref="IndexWriter"/> object </returns>
        private IndexWriter BuildIndexWriter(string indexDirectoryPath)
        {
            // the index directoryFactory
            var fsDirectory = _directoryFactory.GetDirectory(indexDirectoryPath);

            // the index analyzer
            var baseAnalyzer = _analyzerFactory.GetAnalyzer();

            // the actual index writter
            var indexWriter = _indexWriterFactory.BuildIndexWritter(fsDirectory, baseAnalyzer);
            return indexWriter;
        }

        /// <summary>
        /// Builds a default index reader.
        /// </summary>
        /// <param name="indexDirectoryPath">The path of the directory where the index is located</param>
        /// <returns>Default <see cref="IndexReader"/> object</returns>
        private IndexReader BuildIndexReader(string indexDirectoryPath)
        {
            var fsDirectory = _directoryFactory.GetDirectory(indexDirectoryPath);

            var reader = _indexReaderFactory.GetIndexReader(fsDirectory, false);
            return reader;
        }

        #endregion

        #region Factory Configuration

        /// <summary>
        /// Used to change the analyzerFactory used to create the analyzers in run-time depending
        /// on indexing requirments
        /// </summary>
        /// <param name="analyzerFactory">The analyzer analyzerFactory used in </param>
        public void SetAnalyzerFactory(IAnalyzerFactory<Analyzer> analyzerFactory)
        {
            _analyzerFactory = analyzerFactory;

        }

        /// <summary>
        /// Used to change the analyzerFactory used to create the lucene directories in run-time  depending on the indexing requirments.
        /// </summary>
        /// <param name="directoryFactory">The new directory factory.</param>
        public void SetDirectoryFactory(IDirectoryFactory<Directory> directoryFactory)
        {
            _directoryFactory = directoryFactory;
        }

        #endregion
    }
}