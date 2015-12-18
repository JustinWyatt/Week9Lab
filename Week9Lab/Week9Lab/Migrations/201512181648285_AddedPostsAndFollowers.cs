namespace Week9Lab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostsAndFollowers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followees", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Followers", new[] { "User_Id" });
            DropIndex("dbo.Followees", new[] { "User_Id" });
            DropTable("dbo.Followers");
            DropTable("dbo.Followees");
        }
    }
}
