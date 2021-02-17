namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADdDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Annonces", "date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Annonces", "date");
        }
    }
}
