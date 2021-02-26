using System.Collections.Generic;
using TrocCommunity.Core.Models;

namespace TrocCommunity.WebUi.Service
{
    public interface IUtilisateurService
    {
        Utilisateur CheckLogin(string email, string password);
        void Insert(Utilisateur u);
        void Update(Utilisateur u);

        void SaveChanges();

        Utilisateur FindByEmail(string email);

        Utilisateur FindById(int id);

        List<Utilisateur> FindUtilisateurs();


    }
}