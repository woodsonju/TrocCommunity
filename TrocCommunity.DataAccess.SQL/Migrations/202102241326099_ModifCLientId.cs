namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifCLientId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Livres", "Client_Id", "dbo.Utilisateurs");
            DropIndex("dbo.Livres", new[] { "Client_Id" });
            RenameColumn(table: "dbo.Livres", name: "Client_Id", newName: "ClientId");
            AlterColumn("dbo.Livres", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Livres", "ClientId");
            AddForeignKey("dbo.Livres", "ClientId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livres", "ClientId", "dbo.Utilisateurs");
            DropIndex("dbo.Livres", new[] { "ClientId" });
            AlterColumn("dbo.Livres", "ClientId", c => c.Int());
            RenameColumn(table: "dbo.Livres", name: "ClientId", newName: "Client_Id");
            CreateIndex("dbo.Livres", "Client_Id");
            AddForeignKey("dbo.Livres", "Client_Id", "dbo.Utilisateurs", "Id");
        }
    }
}
