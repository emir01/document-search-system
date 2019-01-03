namespace DSS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentDescriptionAndFKChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "User_Id", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "User_Id" });
            RenameColumn(table: "dbo.Documents", name: "User_Id", newName: "UserId");
            AddColumn("dbo.Documents", "Description", c => c.String());
            AddForeignKey("dbo.Documents", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            CreateIndex("dbo.Documents", "UserId");
            DropColumn("dbo.Documents", "Collaboratrs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Collaboratrs", c => c.String());
            DropIndex("dbo.Documents", new[] { "UserId" });
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropColumn("dbo.Documents", "Description");
            RenameColumn(table: "dbo.Documents", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Documents", "User_Id");
            AddForeignKey("dbo.Documents", "User_Id", "dbo.Users", "Id");
        }
    }
}
