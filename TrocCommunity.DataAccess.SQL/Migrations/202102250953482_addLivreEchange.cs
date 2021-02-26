namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLivreEchange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EchangeLivres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EtatEchange = c.Int(nullable: false),
                        LivreEchangeId = c.Int(nullable: false),
                        DateEchangeCreation = c.DateTime(nullable: false),
                        DateEchangeEffectue = c.DateTime(nullable: false),
                        ClientPropId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.ClientPropId, cascadeDelete: false)
                .ForeignKey("dbo.Livres", t => t.LivreEchangeId, cascadeDelete: false)
                .Index(t => t.LivreEchangeId)
                .Index(t => t.ClientPropId);
            
            AddColumn("dbo.Utilisateurs", "SoldeCompte", c => c.Double());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EchangeLivres", "LivreEchangeId", "dbo.Livres");
            DropForeignKey("dbo.EchangeLivres", "ClientPropId", "dbo.Utilisateurs");
            DropIndex("dbo.EchangeLivres", new[] { "ClientPropId" });
            DropIndex("dbo.EchangeLivres", new[] { "LivreEchangeId" });
            DropColumn("dbo.Utilisateurs", "SoldeCompte");
            DropTable("dbo.EchangeLivres");
        }
    }
}
