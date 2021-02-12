using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Models
{
    public class Client : Utilisateur
    {
        public TableauDeBord TableauDeBord { get; set; }

        public Client()
        {
            TypeUtilisateur = TypeUtilisateur.CLIENT;
            Photo = "imgProfile.png";
        }

        public Client(TableauDeBord tableauDeBord)
        {
            TypeUtilisateur = TypeUtilisateur.CLIENT;
            TableauDeBord = tableauDeBord;
            Photo = "imgProfile.png";
        }

        public Client(string userName, string email, string password, string confirmpwd, DateTime dateNaissance) : base(userName, email, password, confirmpwd, dateNaissance)
        {
            TypeUtilisateur = TypeUtilisateur.CLIENT;
            Photo = "imgProfile.png";
        }

    }


}
