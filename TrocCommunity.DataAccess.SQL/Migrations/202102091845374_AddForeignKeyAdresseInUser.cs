namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyAdresseInUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Utilisateurs", "Adresse_Id", "dbo.Adresses");
            DropIndex("dbo.Utilisateurs", new[] { "Adresse_Id" });
            RenameColumn(table: "dbo.Utilisateurs", name: "Adresse_Id", newName: "AdresseId");
            AlterColumn("dbo.Utilisateurs", "AdresseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Utilisateurs", "AdresseId");
            AddForeignKey("dbo.Utilisateurs", "AdresseId", "dbo.Adresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilisateurs", "AdresseId", "dbo.Adresses");
            DropIndex("dbo.Utilisateurs", new[] { "AdresseId" });
            AlterColumn("dbo.Utilisateurs", "AdresseId", c => c.Int());
            RenameColumn(table: "dbo.Utilisateurs", name: "AdresseId", newName: "Adresse_Id");
            CreateIndex("dbo.Utilisateurs", "Adresse_Id");
            AddForeignKey("dbo.Utilisateurs", "Adresse_Id", "dbo.Adresses", "Id");
        }
    }
}
