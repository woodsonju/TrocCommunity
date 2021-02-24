namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangementNolween : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Adresses", "TypeDeVoie", c => c.String());
            //DropColumn("dbo.Adresses", "FullName");
            //DropColumn("dbo.Adresses", "Longitude");
            //DropColumn("dbo.Adresses", "Latitude");
            //DropColumn("dbo.Adresses", "PlaceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Adresses", "PlaceId", c => c.String());
            AddColumn("dbo.Adresses", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adresses", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adresses", "FullName", c => c.String());
            DropColumn("dbo.Adresses", "TypeDeVoie");
        }
    }
}
