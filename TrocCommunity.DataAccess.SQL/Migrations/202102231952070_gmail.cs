namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gmail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.gmails",
                c => new
                    {
                        To = c.String(nullable: false, maxLength: 128),
                        From = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.To);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.gmails");
        }
    }
}
