namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noHistorique : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EchangeLivres", "ClientPropId", "dbo.Utilisateurs");
            DropForeignKey("dbo.EchangeLivres", "LivreEchangeId", "dbo.Livres");
            DropIndex("dbo.EchangeLivres", new[] { "LivreEchangeId" });
            DropIndex("dbo.EchangeLivres", new[] { "ClientPropId" });
            AlterColumn("dbo.EchangeLivres", "LivreEchangeId", c => c.Int());
            AlterColumn("dbo.EchangeLivres", "ClientPropId", c => c.Int());
            CreateIndex("dbo.EchangeLivres", "LivreEchangeId");
            CreateIndex("dbo.EchangeLivres", "ClientPropId");
            AddForeignKey("dbo.EchangeLivres", "ClientPropId", "dbo.Utilisateurs", "Id");
            AddForeignKey("dbo.EchangeLivres", "LivreEchangeId", "dbo.Livres", "Id");
            DropTable("dbo.ElemHistoriques");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ElemHistoriques",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        ISBN = c.Long(nullable: false),
                        Auteur = c.String(),
                        Image = c.String(),
                        UserNameProp = c.String(),
                        UserNameAch = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.EchangeLivres", "LivreEchangeId", "dbo.Livres");
            DropForeignKey("dbo.EchangeLivres", "ClientPropId", "dbo.Utilisateurs");
            DropIndex("dbo.EchangeLivres", new[] { "ClientPropId" });
            DropIndex("dbo.EchangeLivres", new[] { "LivreEchangeId" });
            AlterColumn("dbo.EchangeLivres", "ClientPropId", c => c.Int(nullable: false));
            AlterColumn("dbo.EchangeLivres", "LivreEchangeId", c => c.Int(nullable: false));
            CreateIndex("dbo.EchangeLivres", "ClientPropId");
            CreateIndex("dbo.EchangeLivres", "LivreEchangeId");
            AddForeignKey("dbo.EchangeLivres", "LivreEchangeId", "dbo.Livres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EchangeLivres", "ClientPropId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
    }
}
