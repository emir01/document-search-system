namespace DSS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationBetweenUsersAndDocs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DocumentCategories", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.DocumentCategories", "Category_Id", "dbo.Categories");
            DropIndex("dbo.DocumentCategories", new[] { "Document_Id" });
            DropIndex("dbo.DocumentCategories", new[] { "Category_Id" });
            CreateTable(
                "dbo.CategoryDocuments",
                c => new
                    {
                        Category_Id = c.Guid(nullable: false),
                        Document_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Document_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Document_Id);
            
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Documents", "User_Id", c => c.Guid());
            AddForeignKey("dbo.Documents", "User_Id", "dbo.Users", "Id");
            CreateIndex("dbo.Documents", "User_Id");
            DropTable("dbo.DocumentCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DocumentCategories",
                c => new
                    {
                        Document_Id = c.Guid(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Document_Id, t.Category_Id });
            
            DropIndex("dbo.CategoryDocuments", new[] { "Document_Id" });
            DropIndex("dbo.CategoryDocuments", new[] { "Category_Id" });
            DropIndex("dbo.Documents", new[] { "User_Id" });
            DropForeignKey("dbo.CategoryDocuments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.CategoryDocuments", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Documents", "User_Id", "dbo.Users");
            DropColumn("dbo.Documents", "User_Id");
            DropColumn("dbo.Users", "Email");
            DropTable("dbo.CategoryDocuments");
            CreateIndex("dbo.DocumentCategories", "Category_Id");
            CreateIndex("dbo.DocumentCategories", "Document_Id");
            AddForeignKey("dbo.DocumentCategories", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DocumentCategories", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
        }
    }
}
