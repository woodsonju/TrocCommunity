namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifLivreForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.Livres", new[] { "Categorie_Id" });
            RenameColumn(table: "dbo.Livres", name: "Categorie_Id", newName: "CatgorieId");
            AlterColumn("dbo.Livres", "CatgorieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Livres", "CatgorieId");
            AddForeignKey("dbo.Livres", "CatgorieId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livres", "CatgorieId", "dbo.Categories");
            DropIndex("dbo.Livres", new[] { "CatgorieId" });
            AlterColumn("dbo.Livres", "CatgorieId", c => c.Int());
            RenameColumn(table: "dbo.Livres", name: "CatgorieId", newName: "Categorie_Id");
            CreateIndex("dbo.Livres", "Categorie_Id");
            AddForeignKey("dbo.Livres", "Categorie_Id", "dbo.Categories", "Id");
        }
    }
}
