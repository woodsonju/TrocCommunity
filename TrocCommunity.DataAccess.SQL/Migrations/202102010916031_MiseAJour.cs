namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MiseAJour : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilisateurs", "typeUtilisateur", c => c.Int());
            DropTable("dbo.UpdateDatabaseAdmins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UpdateDatabaseAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        typeUtilisateur = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Utilisateurs", "typeUtilisateur", c => c.Int(nullable: false));
        }
    }
}
