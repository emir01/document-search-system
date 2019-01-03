namespace DSS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeywordCategoryDocumentMappings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryDocuments", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryDocuments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.KeywordDocuments", "Keyword_Id", "dbo.Keywords");
            DropForeignKey("dbo.KeywordDocuments", "Document_Id", "dbo.Documents");
            DropIndex("dbo.CategoryDocuments", new[] { "Category_Id" });
            DropIndex("dbo.CategoryDocuments", new[] { "Document_Id" });
            DropIndex("dbo.KeywordDocuments", new[] { "Keyword_Id" });
            DropIndex("dbo.KeywordDocuments", new[] { "Document_Id" });
            CreateTable(
                "dbo.CategoriesForDocuments",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.DocumentId })
                .ForeignKey("dbo.Documents", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.KeywordsForDocuments",
                c => new
                    {
                        KeywordId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.KeywordId, t.DocumentId })
                .ForeignKey("dbo.Documents", t => t.KeywordId, cascadeDelete: true)
                .ForeignKey("dbo.Keywords", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.KeywordId)
                .Index(t => t.DocumentId);
            
            DropTable("dbo.CategoryDocuments");
            DropTable("dbo.KeywordDocuments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KeywordDocuments",
                c => new
                    {
                        Keyword_Id = c.Guid(nullable: false),
                        Document_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Keyword_Id, t.Document_Id });
            
            CreateTable(
                "dbo.CategoryDocuments",
                c => new
                    {
                        Category_Id = c.Guid(nullable: false),
                        Document_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Document_Id });
            
            DropIndex("dbo.KeywordsForDocuments", new[] { "DocumentId" });
            DropIndex("dbo.KeywordsForDocuments", new[] { "KeywordId" });
            DropIndex("dbo.CategoriesForDocuments", new[] { "DocumentId" });
            DropIndex("dbo.CategoriesForDocuments", new[] { "CategoryId" });
            DropForeignKey("dbo.KeywordsForDocuments", "DocumentId", "dbo.Keywords");
            DropForeignKey("dbo.KeywordsForDocuments", "KeywordId", "dbo.Documents");
            DropForeignKey("dbo.CategoriesForDocuments", "DocumentId", "dbo.Categories");
            DropForeignKey("dbo.CategoriesForDocuments", "CategoryId", "dbo.Documents");
            DropTable("dbo.KeywordsForDocuments");
            DropTable("dbo.CategoriesForDocuments");
            CreateIndex("dbo.KeywordDocuments", "Document_Id");
            CreateIndex("dbo.KeywordDocuments", "Keyword_Id");
            CreateIndex("dbo.CategoryDocuments", "Document_Id");
            CreateIndex("dbo.CategoryDocuments", "Category_Id");
            AddForeignKey("dbo.KeywordDocuments", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KeywordDocuments", "Keyword_Id", "dbo.Keywords", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryDocuments", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryDocuments", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
