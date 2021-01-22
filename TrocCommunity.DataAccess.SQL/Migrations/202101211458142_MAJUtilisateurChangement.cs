namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJUtilisateurChangement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilisateurs", "Pseudo", c => c.String(maxLength: 20));
            AlterColumn("dbo.Utilisateurs", "Password", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilisateurs", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Utilisateurs", "Pseudo", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
