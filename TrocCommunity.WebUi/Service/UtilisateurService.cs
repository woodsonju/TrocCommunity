using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.Core.Tools;

namespace TrocCommunity.WebUi.Service
{
    public class UtilisateurService : IUtilisateurService
    {
        private IRepository<Utilisateur> repo;

        public UtilisateurService(IRepository<Utilisateur> repo)
        {
            this.repo = repo;
        }

        public Utilisateur CheckLogin(string email, string password)
        {
            Utilisateur u = ((SQLRepositoryUtilisateur)repo).findByEmail(email);
            string pwdCrypt = HashTools.ComputeSha256Hash(password);

            if (u == null || u.Password.Equals(pwdCrypt))
            {
                return null;
            }
            return u;
        }

        public void Insert(Utilisateur u)
        {

            u.Password = HashTools.ComputeSha256Hash(u.Password);

            repo.Insert(u);
        }

        public void Update(Utilisateur u)
        {
            Utilisateur userBD = repo.FindById(u.Id);

            if (!userBD.Password.Equals(u.Password))
            {
                u.Password = HashTools.ComputeSha256Hash(u.Password);
            }
            repo.Update(u);
        }

        public void SaveChanges()
        {
            repo.SaveChanges();
        }

        public Utilisateur FindByEmail(string email)
        {
            return ((SQLRepositoryUtilisateur)repo).findByEmail(email);
        }

        public Utilisateur FindById(int id)
        {
            return repo.FindById(id);
        }
    }
}