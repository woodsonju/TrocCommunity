namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateEdition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livres", "DateEdition", c => c.DateTime(nullable: false));
            DropColumn("dbo.Livres", "Edition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Livres", "Edition", c => c.String());
            DropColumn("dbo.Livres", "DateEdition");
        }
    }
}
