namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProblemAdressMail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.Livres", new[] { "Categorie_Id" });
/*            AddColumn("dbo.Adresses", "FullName", c => c.String());
            AddColumn("dbo.Adresses", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adresses", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adresses", "PlaceId", c => c.String());*/
            AlterColumn("dbo.Livres", "Title", c => c.String());
            AlterColumn("dbo.Livres", "Author", c => c.String());
            AlterColumn("dbo.Livres", "Editor", c => c.String());
            AlterColumn("dbo.Livres", "Image", c => c.String());
            AlterColumn("dbo.Livres", "Categorie_Id", c => c.Int());
            CreateIndex("dbo.Livres", "Categorie_Id");
            AddForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories", "Id");
/*            DropColumn("dbo.Adresses", "TypeDeVoie");
*/        }
        
        public override void Down()
        {
            AddColumn("dbo.Adresses", "TypeDeVoie", c => c.String());
            DropForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.Livres", new[] { "Categorie_Id" });
            AlterColumn("dbo.Livres", "Categorie_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Livres", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Editor", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Adresses", "PlaceId");
            DropColumn("dbo.Adresses", "Latitude");
            DropColumn("dbo.Adresses", "Longitude");
            DropColumn("dbo.Adresses", "FullName");
            CreateIndex("dbo.Livres", "Categorie_Id");
            AddForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
