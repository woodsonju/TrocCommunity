namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProblemContext_MdodifBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.Livres", new[] { "Categorie_Id" });
            AlterColumn("dbo.Livres", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Editor", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Livres", "Categorie_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Livres", "Categorie_Id");
            AddForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.Livres", new[] { "Categorie_Id" });
            AlterColumn("dbo.Livres", "Categorie_Id", c => c.Int());
            AlterColumn("dbo.Livres", "Image", c => c.String());
            AlterColumn("dbo.Livres", "Editor", c => c.String());
            AlterColumn("dbo.Livres", "Author", c => c.String());
            AlterColumn("dbo.Livres", "Title", c => c.String());
            CreateIndex("dbo.Livres", "Categorie_Id");
            AddForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories", "Id");
        }
    }
}
