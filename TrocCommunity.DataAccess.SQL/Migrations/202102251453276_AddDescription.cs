namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livres", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Livres", "Description");
        }
    }
}
