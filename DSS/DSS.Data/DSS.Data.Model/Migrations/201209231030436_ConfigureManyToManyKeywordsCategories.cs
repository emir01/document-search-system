using System.Data.Entity.Migrations;

namespace DSS.Data.Model.Migrations
{
    public partial class ConfigureManyToManyKeywordsCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.Keywords", "Document_Id", "dbo.Documents");
            DropIndex("dbo.Categories", new[] { "Document_Id" });
            DropIndex("dbo.Keywords", new[] { "Document_Id" });
            CreateTable(
                "dbo.DocumentCategories",
                c => new
                    {
                        Document_Id = c.Guid(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Document_Id, t.Category_Id })
                .ForeignKey("dbo.Documents", t => t.Document_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Document_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.KeywordDocuments",
                c => new
                    {
                        Keyword_Id = c.Guid(nullable: false),
                        Document_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Keyword_Id, t.Document_Id })
                .ForeignKey("dbo.Keywords", t => t.Keyword_Id, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Keyword_Id)
                .Index(t => t.Document_Id);
            
            DropColumn("dbo.Categories", "Document_Id");
            DropColumn("dbo.Keywords", "Document_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Keywords", "Document_Id", c => c.Guid());
            AddColumn("dbo.Categories", "Document_Id", c => c.Guid());
            DropIndex("dbo.KeywordDocuments", new[] { "Document_Id" });
            DropIndex("dbo.KeywordDocuments", new[] { "Keyword_Id" });
            DropIndex("dbo.DocumentCategories", new[] { "Category_Id" });
            DropIndex("dbo.DocumentCategories", new[] { "Document_Id" });
            DropForeignKey("dbo.KeywordDocuments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.KeywordDocuments", "Keyword_Id", "dbo.Keywords");
            DropForeignKey("dbo.DocumentCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.DocumentCategories", "Document_Id", "dbo.Documents");
            DropTable("dbo.KeywordDocuments");
            DropTable("dbo.DocumentCategories");
            CreateIndex("dbo.Keywords", "Document_Id");
            CreateIndex("dbo.Categories", "Document_Id");
            AddForeignKey("dbo.Keywords", "Document_Id", "dbo.Documents", "Id");
            AddForeignKey("dbo.Categories", "Document_Id", "dbo.Documents", "Id");
        }
    }
}
