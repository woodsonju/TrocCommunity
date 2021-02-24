namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifClassLivre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livres", "IsExchange", c => c.Boolean(nullable: false));
            AddColumn("dbo.Livres", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Livres", "PointDuLivre", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Livres", "PointDuLivre", c => c.Int(nullable: false));
            DropColumn("dbo.Livres", "Price");
            DropColumn("dbo.Livres", "IsExchange");
        }
    }
}
