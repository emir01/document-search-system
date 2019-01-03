namespace DSS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDownloadAndVotingLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DownloadLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DownloadDate = c.DateTime(nullable: false),
                        User_Id = c.Guid(),
                        Document_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.UpvoteLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UpvoteDate = c.DateTime(nullable: false),
                        UpvoteDesription = c.String(),
                        User_Id = c.Guid(),
                        Document_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.DownvoteLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DownvoteDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        User_Id = c.Guid(),
                        Document_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Document_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DownvoteLogs", new[] { "Document_Id" });
            DropIndex("dbo.DownvoteLogs", new[] { "User_Id" });
            DropIndex("dbo.UpvoteLogs", new[] { "Document_Id" });
            DropIndex("dbo.UpvoteLogs", new[] { "User_Id" });
            DropIndex("dbo.DownloadLogs", new[] { "Document_Id" });
            DropIndex("dbo.DownloadLogs", new[] { "User_Id" });
            DropForeignKey("dbo.DownvoteLogs", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.DownvoteLogs", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UpvoteLogs", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.UpvoteLogs", "User_Id", "dbo.Users");
            DropForeignKey("dbo.DownloadLogs", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.DownloadLogs", "User_Id", "dbo.Users");
            DropTable("dbo.DownvoteLogs");
            DropTable("dbo.UpvoteLogs");
            DropTable("dbo.DownloadLogs");
        }
    }
}
