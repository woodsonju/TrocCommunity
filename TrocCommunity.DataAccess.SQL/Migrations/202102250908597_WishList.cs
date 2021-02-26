namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishList : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.WishLists", "Title", c => c.String());
            //AddColumn("dbo.WishLists", "Author", c => c.String());
            //AddColumn("dbo.WishLists", "Image", c => c.String());
            //AddColumn("dbo.WishLists", "ClientOwner", c => c.Int(nullable: false));
            //AddColumn("dbo.WishLists", "IdBook", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WishLists", "IdBook");
            DropColumn("dbo.WishLists", "ClientOwner");
            DropColumn("dbo.WishLists", "Image");
            DropColumn("dbo.WishLists", "Author");
            DropColumn("dbo.WishLists", "Title");
        }
    }
}
