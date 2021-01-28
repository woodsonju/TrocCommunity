namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using TrocCommunity.Core.Models;

    public partial class AjoutAdminUser : DbMigration
    {
        public override void Up()
        {

            // MODIFICATION DATABASE POUR OBTENIR CELLE DE WOODSON

            //RenameColumn("dbo.Utilisateurs", "Pseudo", "UserName");
            //DropColumn("dbo.Utilisateurs","Statut");
            //Sql("ALTER TABLE Utilisateurs ALTER COLUMN LastName NVARCHAR(MAX) NULL");
            //Sql("ALTER TABLE Utilisateurs ALTER COLUMN FirstName NVARCHAR(MAX) NULL");
            //Sql("ALTER TABLE Utilisateurs ALTER COLUMN PhoneNumber NVARCHAR(50) NULL");

        }
        
        public override void Down()
        {
        }
    }
}
