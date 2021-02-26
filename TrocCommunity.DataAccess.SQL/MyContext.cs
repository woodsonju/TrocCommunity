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

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<BibliothequeVirtuelle> BibliothequeVirtuelle { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<GestionnaireDePoint> GestionnaireDePoints { get; set; }
        public virtual DbSet<LigneDeCommande> LigneDeCommandes { get; set; }
        public virtual DbSet<Livre> Livres { get; set; }
        public virtual DbSet<EchangeLivre> EchangeLivres { get; set; }
        public virtual DbSet<TableauDeBord> TableauDeBords { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }






    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}