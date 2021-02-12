namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilisateurs", "TypeUtilisateur", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilisateurs", "TypeUtilisateur", c => c.Int());
        }
    }
}
