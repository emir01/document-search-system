namespace DSS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesAndUserFeatureTiers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Alias = c.String(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserFeatureTiers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TierName = c.String(),
                        TierAlias = c.String(),
                        TierDescription = c.String(),
                        DocumentDownloadsPerDay = c.Int(nullable: false),
                        DocumentUpvotesPerDay = c.Int(nullable: false),
                        DocumentDownvotesPerDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "UserFeatureTier_Id", c => c.Guid());
            AddForeignKey("dbo.Users", "UserFeatureTier_Id", "dbo.UserFeatureTiers", "Id");
            CreateIndex("dbo.Users", "UserFeatureTier_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Roles", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "UserFeatureTier_Id" });
            DropForeignKey("dbo.Roles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "UserFeatureTier_Id", "dbo.UserFeatureTiers");
            DropColumn("dbo.Users", "UserFeatureTier_Id");
            DropTable("dbo.UserFeatureTiers");
            DropTable("dbo.Roles");
        }
    }
}
