using System;
using System.Data.Entity;
using System.Linq;
using TrocCommunity.Core.Models;

namespace TrocCommunity.DataAccess.SQL
{
    public class MyContext : DbContext
    {
   
        public MyContext()
            : base("name=MyContext")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<BibliothequeVirtuelle> BibliothequeVirtuelle { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<GestionnaireDePoint> GestionnaireDePoints { get; set; }
        public DbSet<LigneDeCommande> LigneDeCommandes { get; set; }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<TableauDeBord> TableauDeBords { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<WishList> WishLists { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}