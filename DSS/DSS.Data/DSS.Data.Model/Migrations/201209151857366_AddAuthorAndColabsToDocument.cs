using System.Data.Entity.Migrations;

namespace DSS.Data.Model.Migrations
{
    public partial class AddAuthorAndColabsToDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "AuthorName", c => c.String());
            AddColumn("dbo.Documents", "Collaboratrs", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Collaboratrs");
            DropColumn("dbo.Documents", "AuthorName");
        }
    }
}
