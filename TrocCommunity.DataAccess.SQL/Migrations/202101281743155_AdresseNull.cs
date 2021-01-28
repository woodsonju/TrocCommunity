namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdresseNull : DbMigration
    {
        public override void Up()
        {
            //Sql("ALTER TABLE Utilisateurs ALTER COLUMN Adresse_Id INT NULL");

        }
        
        public override void Down()
        {
        }
    }
}
