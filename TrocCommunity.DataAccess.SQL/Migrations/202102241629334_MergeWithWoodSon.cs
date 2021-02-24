namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeWithWoodSon : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilisateurs", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Utilisateurs", "Confirmpwd", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilisateurs", "Confirmpwd", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Utilisateurs", "Password", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
