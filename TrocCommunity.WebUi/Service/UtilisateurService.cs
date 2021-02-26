using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.Core.Tools;
using TrocCommunity.WebUi.ExceptionTC;
using TrocCommunity.DataAccess.SQL;

namespace TrocCommunity.WebUi.Service
{
    public class UtilisateurService : IUtilisateurService
    {
        private IRepository<Utilisateur> repo;

        private IRepository<Utilisateur> repoUserCustomize;

        public UtilisateurService()
        {
            repoUserCustomize = new SQLRepositoryUtilisateur(new MyContext());
        }

        public UtilisateurService(IRepository<Utilisateur> repo)
        {
            this.repo = repo;
        }

        public Utilisateur CheckLogin(string email, string password)
        {
            Utilisateur u = ((SQLRepositoryUtilisateur)repo).findByEmail(email);

            string pwdCrypt = password;

            if (u.TypeUtilisateur != TypeUtilisateur.ADMIN)
            {
                pwdCrypt = HashTools.ComputeSha256Hash(password);
            }


            if (u == null || !u.Password.Equals(pwdCrypt))
            {
                return null;
            }
            return u;
        }

        public void Insert(Utilisateur u)
        {
      

/*            if(((SQLRepositoryUtilisateur)repoUserCustomize).findByEmail(u.Email) != null)
            {
                throw new UserNameIsException("Le UserName existe déjà. Veuillez choisir un autre UserName!!");
            }

            if (((SQLRepositoryUtilisateur)repoUserCustomize).findByUserName(u.UserName) != null)
            {
                throw new EmailIsException("L'adresse mail existe déjà. Veuillez choisir un autre adresse mail!!");
            }*/

            List<Utilisateur> utilisateursDao = FindUtilisateurs();

            for(int i=0; i<utilisateursDao.Count; i++)
            {
                if (u.UserName == utilisateursDao[i].UserName)
                {
                    throw new UserNameIsException("Le UserName existe déjà. Veuillez choisir un autre UserName!!");
                }

                if (u.Email == utilisateursDao[i].Email)
                {
                    throw new EmailIsException("L'adresse mail existe déjà. Veuillez choisir un autre adresse mail!!");
                } 

            }
            
            u.Password = HashTools.ComputeSha256Hash(u.Password);

            repo.Insert(u);
        }

        public void Update(Utilisateur u)
        {
            Utilisateur userBD = repo.FindById(u.Id);

            if (!userBD.Password.Equals(u.Confirmpwd))
            {
                u.Password = HashTools.ComputeSha256Hash(u.Confirmpwd);
                u.Confirmpwd = HashTools.ComputeSha256Hash(u.Confirmpwd);
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

        public List<Utilisateur> FindUtilisateurs()
        {
            return repo.Collection().ToList();
        }
    }
}