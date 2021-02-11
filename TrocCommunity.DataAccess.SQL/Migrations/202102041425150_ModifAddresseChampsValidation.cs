namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifAddresseChampsValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Adresses", "TypeDeVoie", c => c.String());
            AlterColumn("dbo.Adresses", "NomDeVoie", c => c.String());
            AlterColumn("dbo.Adresses", "CodePostale", c => c.String());
            AlterColumn("dbo.Adresses", "Ville", c => c.String());
            AlterColumn("dbo.Adresses", "Pays", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Adresses", "Pays", c => c.String(nullable: false));
            AlterColumn("dbo.Adresses", "Ville", c => c.String(nullable: false));
            AlterColumn("dbo.Adresses", "CodePostale", c => c.String(nullable: false));
            AlterColumn("dbo.Adresses", "NomDeVoie", c => c.String(nullable: false));
            AlterColumn("dbo.Adresses", "TypeDeVoie", c => c.String(nullable: false));
        }
    }
}
