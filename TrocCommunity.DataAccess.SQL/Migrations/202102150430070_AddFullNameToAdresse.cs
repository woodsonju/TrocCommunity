namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullNameToAdresse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adresses", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adresses", "FullName");
        }
    }
}
