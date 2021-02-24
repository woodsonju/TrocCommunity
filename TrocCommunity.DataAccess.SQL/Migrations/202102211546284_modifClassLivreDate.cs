namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifClassLivreDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn ("dbo.Livres", "DateEdition", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Livres", "DateEdition", c => c.DateTime(nullable: false));
        }
    }
}
