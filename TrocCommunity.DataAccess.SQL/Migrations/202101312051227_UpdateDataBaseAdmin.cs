namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataBaseAdmin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UpdateDatabaseAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        typeUtilisateur = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UpdateDatabaseAdmins");
        }
    }
}
