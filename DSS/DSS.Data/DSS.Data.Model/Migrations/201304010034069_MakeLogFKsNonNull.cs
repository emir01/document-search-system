namespace DSS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeLogFKsNonNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DownloadLogs", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.UpvoteLogs", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.DownvoteLogs", "Document_Id", "dbo.Documents");
            DropIndex("dbo.DownloadLogs", new[] { "Document_Id" });
            DropIndex("dbo.UpvoteLogs", new[] { "Document_Id" });
            DropIndex("dbo.DownvoteLogs", new[] { "Document_Id" });
            AlterColumn("dbo.DownloadLogs", "Document_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.UpvoteLogs", "Document_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.DownvoteLogs", "Document_Id", c => c.Guid(nullable: false));
            AddForeignKey("dbo.DownloadLogs", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UpvoteLogs", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DownvoteLogs", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            CreateIndex("dbo.DownloadLogs", "Document_Id");
            CreateIndex("dbo.UpvoteLogs", "Document_Id");
            CreateIndex("dbo.DownvoteLogs", "Document_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DownvoteLogs", new[] { "Document_Id" });
            DropIndex("dbo.UpvoteLogs", new[] { "Document_Id" });
            DropIndex("dbo.DownloadLogs", new[] { "Document_Id" });
            DropForeignKey("dbo.DownvoteLogs", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.UpvoteLogs", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.DownloadLogs", "Document_Id", "dbo.Documents");
            AlterColumn("dbo.DownvoteLogs", "Document_Id", c => c.Guid());
            AlterColumn("dbo.UpvoteLogs", "Document_Id", c => c.Guid());
            AlterColumn("dbo.DownloadLogs", "Document_Id", c => c.Guid());
            CreateIndex("dbo.DownvoteLogs", "Document_Id");
            CreateIndex("dbo.UpvoteLogs", "Document_Id");
            CreateIndex("dbo.DownloadLogs", "Document_Id");
            AddForeignKey("dbo.DownvoteLogs", "Document_Id", "dbo.Documents", "Id");
            AddForeignKey("dbo.UpvoteLogs", "Document_Id", "dbo.Documents", "Id");
            AddForeignKey("dbo.DownloadLogs", "Document_Id", "dbo.Documents", "Id");
        }
    }
}
