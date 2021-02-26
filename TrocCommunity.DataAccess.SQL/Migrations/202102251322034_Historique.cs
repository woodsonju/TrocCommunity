namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Historique : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElemHistoriques",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        ISBN = c.Long(nullable: false),
                        Auteur = c.String(),
                        Image = c.String(),
                        UserNameProp = c.String(),
                        UserNameAch = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ElemHistoriques");
        }
    }
}
