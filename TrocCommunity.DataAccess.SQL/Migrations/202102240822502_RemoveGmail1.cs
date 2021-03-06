namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGmail1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.gmails");
        }
        
        public override void Down()
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
    }
}
