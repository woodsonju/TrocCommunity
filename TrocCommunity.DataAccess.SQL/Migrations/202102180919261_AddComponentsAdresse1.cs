namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComponentsAdresse1 : DbMigration
    {
        public override void Up()
        {
/*            AddColumn("dbo.Adresses", "FullName", c => c.String());
            AddColumn("dbo.Adresses", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adresses", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adresses", "PlaceId", c => c.String());*/
            DropColumn("dbo.Adresses", "TypeDeVoie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Adresses", "TypeDeVoie", c => c.String());
            DropColumn("dbo.Adresses", "PlaceId");
            DropColumn("dbo.Adresses", "Latitude");
            DropColumn("dbo.Adresses", "Longitude");
            DropColumn("dbo.Adresses", "FullName");
        }
    }
}
