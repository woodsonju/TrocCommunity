namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTitleBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Annonces", "TableauDeBord_Id", "dbo.TableauDeBords");
            DropForeignKey("dbo.Annonces", "Client_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Annonces", "Livre_Id", "dbo.Livres");
            DropIndex("dbo.Annonces", new[] { "TableauDeBord_Id" });
            DropIndex("dbo.Annonces", new[] { "Client_Id" });
            DropIndex("dbo.Annonces", new[] { "Livre_Id" });
            AddColumn("dbo.Livres", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Livres", "ISBN", c => c.Long(nullable: false));
            AddColumn("dbo.Livres", "EtatDuLivre", c => c.Int(nullable: false));
            AddColumn("dbo.Livres", "PointDuLivre", c => c.Int(nullable: false));
            AddColumn("dbo.Livres", "Client_Id", c => c.Int());
            AlterColumn("dbo.Livres", "Edition", c => c.String());
            AlterColumn("dbo.Livres", "Language", c => c.String());
            CreateIndex("dbo.Livres", "Client_Id");
            AddForeignKey("dbo.Livres", "Client_Id", "dbo.Utilisateurs", "Id");
            DropColumn("dbo.Livres", "BarCode");
            DropTable("dbo.Annonces");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Annonces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EtatDuLivre = c.Int(nullable: false),
                        PointDuLivre = c.Int(nullable: false),
                        TableauDeBord_Id = c.Int(),
                        Client_Id = c.Int(),
                        Livre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Livres", "BarCode", c => c.Long(nullable: false));
            DropForeignKey("dbo.Livres", "Client_Id", "dbo.Utilisateurs");
            DropIndex("dbo.Livres", new[] { "Client_Id" });
            AlterColumn("dbo.Livres", "Language", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Edition", c => c.String(nullable: false));
            DropColumn("dbo.Livres", "Client_Id");
            DropColumn("dbo.Livres", "PointDuLivre");
            DropColumn("dbo.Livres", "EtatDuLivre");
            DropColumn("dbo.Livres", "ISBN");
            DropColumn("dbo.Livres", "Title");
            CreateIndex("dbo.Annonces", "Livre_Id");
            CreateIndex("dbo.Annonces", "Client_Id");
            CreateIndex("dbo.Annonces", "TableauDeBord_Id");
            AddForeignKey("dbo.Annonces", "Livre_Id", "dbo.Livres", "Id");
            AddForeignKey("dbo.Annonces", "Client_Id", "dbo.Utilisateurs", "Id");
            AddForeignKey("dbo.Annonces", "TableauDeBord_Id", "dbo.TableauDeBords", "Id");
        }
    }
}
