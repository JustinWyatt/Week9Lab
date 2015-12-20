namespace Week9Lab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedFollowerTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Followers", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Followers", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            AddColumn("dbo.Followers", "FollwerToWhoIsBeingFollowed_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Followers", "WhoIsBeingFollowed_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Followers", "FollwerToWhoIsBeingFollowed_Id");
            CreateIndex("dbo.Followers", "WhoIsBeingFollowed_Id");
            AddForeignKey("dbo.Followers", "FollwerToWhoIsBeingFollowed_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Followers", "WhoIsBeingFollowed_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followers", "WhoIsBeingFollowed_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followers", "FollwerToWhoIsBeingFollowed_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Followers", new[] { "WhoIsBeingFollowed_Id" });
            DropIndex("dbo.Followers", new[] { "FollwerToWhoIsBeingFollowed_Id" });
            DropColumn("dbo.Followers", "WhoIsBeingFollowed_Id");
            DropColumn("dbo.Followers", "FollwerToWhoIsBeingFollowed_Id");
            RenameIndex(table: "dbo.Followers", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Followers", name: "ApplicationUser_Id", newName: "User_Id");
        }
    }
}
