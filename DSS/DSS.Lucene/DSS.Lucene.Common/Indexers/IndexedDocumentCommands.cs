using System;
using DSS.Lucene.Common.Entities;
using DSS.Lucene.Common.Utilities;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace DSS.Lucene.Common.Indexers
{
    /// <summary>
    /// The implementation of the IndexedDocument writer over the Indexed Document Class
    /// </summary>
    public class IndexedDocumentCommands : ILuceneIndexCommands<IndexedDocument>
    {
        /// <summary>
        /// Writes a data object to the provided index via the Index writter.
        /// </summary>
        /// <param name="data">The data containing all the information to be stored in the index.</param>
        /// <param name="index">The index where the data will be stored</param>
        public void AddToIndex(IndexedDocument data, IndexWriter index)
        {
            var luceneDocument = new Document();
           
            // create the fields that will be stored in the inde
            var idField = new Field(IndexedDocumentFieldDictionary.Id, data.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
            var contentField = new Field(IndexedDocumentFieldDictionary.Content,data.Contents,Field.Store.YES, Field.Index.ANALYZED);
            var titleField = new Field(IndexedDocumentFieldDictionary.Title, data.Title, Field.Store.YES, Field.Index.ANALYZED);
            var uploadDateField = new Field(IndexedDocumentFieldDictionary.UploadDate, data.DateCreated.ToString(), Field.Store.YES, Field.Index.ANALYZED);
            var indexedDateField = new Field(IndexedDocumentFieldDictionary.IndexDate, DateTime.Now.ToString(), Field.Store.YES, Field.Index.ANALYZED);
           
            // add the fields to the document
            luceneDocument.Add(idField);
            luceneDocument.Add(contentField);
            luceneDocument.Add(titleField);
            luceneDocument.Add(uploadDateField);
            luceneDocument.Add(indexedDateField);

            index.AddDocument(luceneDocument);

            index.Optimize();
            index.Dispose();
        }

        /// <summary>
        /// Remove a document fro the index based on a given term.
        /// </summary>
        /// <param name="data">The abstract document representation for the document to be removed from the index</param>
        /// <param name="indexReader">The reader used to remove a document form the index</param>
        public void RemoveFromIndex(IndexedDocument data, IndexReader indexReader)
        {
            // create the term object that will be used to remove the indexe document based on the id field
            var removalTerm = new Term(IndexedDocumentFieldDictionary.Id, data.Id.ToString());
            indexReader.DeleteDocuments(removalTerm);
        }

        /// <summary>
        /// Optimizes an index using the given index reader, that should already be setup on the index we want to optimize
        /// </summary>
        /// <param name="indexWriter">The index reader for the index we want to optimize/</param>
        public void OptimizeIndex(IndexWriter indexWriter)
        {
            indexWriter.Optimize();
        }
    }
}   
