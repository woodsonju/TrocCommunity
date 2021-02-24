namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutAvanceDePointsLivre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livres", "AvancePoints", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Livres", "AvancePoints");
        }
    }
}
