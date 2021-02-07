namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        UserName = c.String(nullable: false, maxLength: 20),
                        PhoneNumber = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                        Confirmpwd = c.String(nullable: false, maxLength: 20),
                        DateNaissance = c.DateTime(nullable: false),
                        TypeUtilisateur = c.Int(nullable: false),
                        Photo = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        TableauDeBord_Id = c.Int(),
                        TableauDeBord_Id1 = c.Int(),
                        Adresse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableauDeBords", t => t.TableauDeBord_Id)
                .ForeignKey("dbo.TableauDeBords", t => t.TableauDeBord_Id1)
                .ForeignKey("dbo.Adresses", t => t.Adresse_Id)
                .Index(t => t.TableauDeBord_Id)
                .Index(t => t.TableauDeBord_Id1)
                .Index(t => t.Adresse_Id);
            
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeDeVoie = c.String(nullable: false),
                        NomDeVoie = c.String(nullable: false),
                        NumVoie = c.Int(nullable: false),
                        CodePostale = c.String(nullable: false),
                        Ville = c.String(nullable: false),
                        Pays = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableauDeBords", t => t.TableauDeBord_Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Client_Id)
                .ForeignKey("dbo.Livres", t => t.Livre_Id)
                .Index(t => t.TableauDeBord_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Livre_Id);
            
            CreateTable(
                "dbo.TableauDeBords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BibliothequeVirtuelle_Id = c.Int(),
                        Client_Id = c.Int(),
                        GestionnaireDePoint_Id = c.Int(),
                        LigneDeCommande_Id = c.Int(),
                        WishList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BibliothequeVirtuelles", t => t.BibliothequeVirtuelle_Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Client_Id)
                .ForeignKey("dbo.GestionnaireDePoints", t => t.GestionnaireDePoint_Id)
                .ForeignKey("dbo.LigneDeCommandes", t => t.LigneDeCommande_Id)
                .ForeignKey("dbo.WishLists", t => t.WishList_Id)
                .Index(t => t.BibliothequeVirtuelle_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.GestionnaireDePoint_Id)
                .Index(t => t.LigneDeCommande_Id)
                .Index(t => t.WishList_Id);
            
            CreateTable(
                "dbo.BibliothequeVirtuelles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Livres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Editor = c.String(),
                        Edition = c.String(),
                        BarCode = c.Long(nullable: false),
                        Volume = c.Int(nullable: false),
                        Language = c.String(),
                        Image = c.String(),
                        Disponible = c.Boolean(nullable: false),
                        Categorie_Id = c.Int(),
                        BibliothequeVirtuelle_Id = c.Int(),
                        TableauDeBord_Id = c.Int(),
                        WishList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Categorie_Id)
                .ForeignKey("dbo.BibliothequeVirtuelles", t => t.BibliothequeVirtuelle_Id)
                .ForeignKey("dbo.TableauDeBords", t => t.TableauDeBord_Id)
                .ForeignKey("dbo.WishLists", t => t.WishList_Id)
                .Index(t => t.Categorie_Id)
                .Index(t => t.BibliothequeVirtuelle_Id)
                .Index(t => t.TableauDeBord_Id)
                .Index(t => t.WishList_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomCategorie = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GestionnaireDePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PointRecus = c.Int(nullable: false),
                        PointUtilises = c.Int(nullable: false),
                        Solde = c.Int(nullable: false),
                        AvanceDePoint = c.Int(nullable: false),
                        PointDisponible = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LigneDeCommandes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NbreLivreEnvoye = c.Int(nullable: false),
                        NbreLivreRecu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilisateurs", "Adresse_Id", "dbo.Adresses");
            DropForeignKey("dbo.Annonces", "Livre_Id", "dbo.Livres");
            DropForeignKey("dbo.Annonces", "Client_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Utilisateurs", "TableauDeBord_Id1", "dbo.TableauDeBords");
            DropForeignKey("dbo.TableauDeBords", "WishList_Id", "dbo.WishLists");
            DropForeignKey("dbo.Livres", "WishList_Id", "dbo.WishLists");
            DropForeignKey("dbo.Livres", "TableauDeBord_Id", "dbo.TableauDeBords");
            DropForeignKey("dbo.TableauDeBords", "LigneDeCommande_Id", "dbo.LigneDeCommandes");
            DropForeignKey("dbo.TableauDeBords", "GestionnaireDePoint_Id", "dbo.GestionnaireDePoints");
            DropForeignKey("dbo.Utilisateurs", "TableauDeBord_Id", "dbo.TableauDeBords");
            DropForeignKey("dbo.TableauDeBords", "Client_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.TableauDeBords", "BibliothequeVirtuelle_Id", "dbo.BibliothequeVirtuelles");
            DropForeignKey("dbo.Livres", "BibliothequeVirtuelle_Id", "dbo.BibliothequeVirtuelles");
            DropForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories");
            DropForeignKey("dbo.Annonces", "TableauDeBord_Id", "dbo.TableauDeBords");
            DropIndex("dbo.Livres", new[] { "WishList_Id" });
            DropIndex("dbo.Livres", new[] { "TableauDeBord_Id" });
            DropIndex("dbo.Livres", new[] { "BibliothequeVirtuelle_Id" });
            DropIndex("dbo.Livres", new[] { "Categorie_Id" });
            DropIndex("dbo.TableauDeBords", new[] { "WishList_Id" });
            DropIndex("dbo.TableauDeBords", new[] { "LigneDeCommande_Id" });
            DropIndex("dbo.TableauDeBords", new[] { "GestionnaireDePoint_Id" });
            DropIndex("dbo.TableauDeBords", new[] { "Client_Id" });
            DropIndex("dbo.TableauDeBords", new[] { "BibliothequeVirtuelle_Id" });
            DropIndex("dbo.Annonces", new[] { "Livre_Id" });
            DropIndex("dbo.Annonces", new[] { "Client_Id" });
            DropIndex("dbo.Annonces", new[] { "TableauDeBord_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "Adresse_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "TableauDeBord_Id1" });
            DropIndex("dbo.Utilisateurs", new[] { "TableauDeBord_Id" });
            DropTable("dbo.WishLists");
            DropTable("dbo.LigneDeCommandes");
            DropTable("dbo.GestionnaireDePoints");
            DropTable("dbo.Categories");
            DropTable("dbo.Livres");
            DropTable("dbo.BibliothequeVirtuelles");
            DropTable("dbo.TableauDeBords");
            DropTable("dbo.Annonces");
            DropTable("dbo.Adresses");
            DropTable("dbo.Utilisateurs");
        }
    }
}
