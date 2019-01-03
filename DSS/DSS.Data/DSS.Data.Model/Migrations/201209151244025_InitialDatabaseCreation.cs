using System.Data.Entity.Migrations;

namespace DSS.Data.Model.Migrations
{
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Alias = c.String(),
                        Document_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Path = c.String(),
                        DateUploaded = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(),
                        IsIndexed = c.Boolean(nullable: false),
                        DateIndexed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Keywords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Alias = c.String(),
                        Document_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .Index(t => t.Document_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Keywords", new[] { "Document_Id" });
            DropIndex("dbo.Categories", new[] { "Document_Id" });
            DropForeignKey("dbo.Keywords", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.Categories", "Document_Id", "dbo.Documents");
            DropTable("dbo.Keywords");
            DropTable("dbo.Documents");
            DropTable("dbo.Categories");
            DropTable("dbo.Users");
        }
    }
}
