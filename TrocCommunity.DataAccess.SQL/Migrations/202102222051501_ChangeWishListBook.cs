namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeWishListBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WishLists", "IdBook", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WishLists", "IdBook");
        }
    }
}
