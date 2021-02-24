namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifLivreString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Livres", "DateEdition", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Livres", "DateEdition", c => c.Int(nullable: false));
        }
    }
}
