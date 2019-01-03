using System;

namespace DSS.Lucene.Common.Entities
{
    /// <summary>
    /// The document base class describing an abstraction over index values in lucene
    /// </summary>
    public class IndexedDocument
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Contents { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateIndexed { get; set; }
    }
}
