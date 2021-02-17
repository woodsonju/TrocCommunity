namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADdNameBookAndForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Annonces", "Livre_Id", "dbo.Livres");
            DropIndex("dbo.Annonces", new[] { "Livre_Id" });
            RenameColumn(table: "dbo.Annonces", name: "Livre_Id", newName: "LivreId");
            AddColumn("dbo.Livres", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Annonces", "LivreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Annonces", "LivreId");
            AddForeignKey("dbo.Annonces", "LivreId", "dbo.Livres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Annonces", "LivreId", "dbo.Livres");
            DropIndex("dbo.Annonces", new[] { "LivreId" });
            AlterColumn("dbo.Annonces", "LivreId", c => c.Int());
            DropColumn("dbo.Livres", "Title");
            RenameColumn(table: "dbo.Annonces", name: "LivreId", newName: "Livre_Id");
            CreateIndex("dbo.Annonces", "Livre_Id");
            AddForeignKey("dbo.Annonces", "Livre_Id", "dbo.Livres", "Id");
        }
    }
}
