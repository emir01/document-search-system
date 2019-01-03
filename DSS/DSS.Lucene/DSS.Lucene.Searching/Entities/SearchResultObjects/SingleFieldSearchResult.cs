using Lucene.Net.Documents;
using Lucene.Net.Search;

namespace DSS.Lucene.Searching.Entities.SearchResultObjects
{
    /// <summary>
    /// Contains the Top Docs search results from a single field Lucene index query.
    /// Also provides functionality to get the actual documents.
    /// </summary>
    public class SingleFieldSearchResult
    {
        #region Properties

        /// <summary>
        /// The actual Top documents returned from the query.
        /// </summary>
        public TopDocs TopDocs { get; set; }

        /// <summary>
        /// A reference to the searches used to execute the query which in turn is used to get
        /// references to the actual Documents off the results.
        /// </summary>
        private readonly IndexSearcher _searcher;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a SingleFieldSearchResult using the given searcher.
        /// </summary>
        /// <param name="searcher"></param>
        public SingleFieldSearchResult(IndexSearcher searcher)
        {
            _searcher = searcher;
        }

        #endregion

        /// <summary>
        /// Return the document asosiated with the given Score Doc.
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        public Document GetDocument(ScoreDoc hit)
        {
            // get the document using the searcher
            var doc = _searcher.Doc(hit.Doc);
            return doc;
        }
    }
}
