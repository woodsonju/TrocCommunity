namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelModif1 : DbMigration
    {
        public override void Up()
        {
            /*AddColumn("dbo.Livres", "WishList_Id", c => c.Int());
            CreateIndex("dbo.Livres", "WishList_Id");
            AddForeignKey("dbo.Livres", "WishList_Id", "dbo.WishLists", "Id");*/
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livres", "WishList_Id", "dbo.WishLists");
            DropIndex("dbo.Livres", new[] { "WishList_Id" });
            DropColumn("dbo.Livres", "WishList_Id");
        }
    }
}
