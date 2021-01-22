namespace TrocCommunity.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using TrocCommunity.Core.Models;

    public partial class AddAdminClients : DbMigration
    {
        public override void Up()
        {
            var context = new MyContext();

            Adresse adresse = new Adresse() { CodePostale="75005", NomDeVoie="LaForet", NumVoie=4, TypeDeVoie="Rue", Ville = "Paris", Pays = "France" };

            context.Adresses.Add(adresse);

            context.SaveChanges();

            adresse = context.Adresses.Find(1);

            Client client1 = new Client() { FirstName = "Marc", LastName = "Pereira", DateNaissance = DateTime.Now, Email = "pereiramarc@hotmail.fr", Password = "clientTest@", Confirmpwd= "clientTest@", typeUtilisateur = TypeUtilisateur.CLIENT, Adresse = adresse, PhoneNumber="0620202020", Statut=false, Pseudo="PseudoUtilisateur"  };

            Admin admin = new Admin() { FirstName = "adhe", LastName = "mine", DateNaissance = DateTime.Now, Email = "admin@hotmail.fr", Password = "adminTest@", Confirmpwd = "adminTest@", typeUtilisateur = TypeUtilisateur.ADMIN, Adresse = adresse, PhoneNumber = "0620202020", Statut = true, Pseudo = "PseudoAdmin" };

            

            context.Clients.Add(client1);
            context.Admins.Add(admin);

            context.SaveChanges();

        }
        
        public override void Down()
        {
        }
    }
}
