using System;

namespace DSS.Common.ViewModels.Documents
{
    /// <summary>
    /// View Model containing statistics for a given Document, processed from the Document General and various log information
    /// </summary>
    public class DocumentStatsViewModel
    {
        public int TotalDownloads { get; set; }

        public int TotalUpvotes { get; set; }

        public int TotalDownvotes { get; set; }

        public string UploadedUsername { get; set; }

        public string UploadDateString { get; set; }

        public DateTime DateUploaded { get; set; }
    }
}
