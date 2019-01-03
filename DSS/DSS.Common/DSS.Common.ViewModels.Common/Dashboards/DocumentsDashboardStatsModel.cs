using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Common.ViewModels.Dashboards
{
   /// <summary>
   /// Strongly type object containing the 
   /// </summary>
    public class DocumentsDashboardStatsModel
    {
        /// <summary>
        /// Number of docsuments added to the index
        /// </summary>
        public int DocumentsInIndex { get; set; }

        /// <summary>
        /// The number of dcomuents in the DSS system marked as indexed
        /// and ready for download
        /// </summary>
        public int SearchableDocuments { get; set; }

        /// <summary>
        /// The total number of uploaded documents in the system both indexed and non indexed
        /// </summary>
        public int TotalUploadedDocuments { get; set; }

        /// <summary>
        ///  Flag for the index optimization state
        /// </summary>
        public string IsIndexedOptimized { get; set; }
    }
}
