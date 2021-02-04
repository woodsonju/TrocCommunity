using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    public class SQLRepositoryUtilisateur : SQLRepository<Utilisateur>
    {
        public SQLRepositoryUtilisateur(MyContext DataContext) : base(DataContext)
        {
        }

        public Utilisateur findByEmail(string email)
        {
            Utilisateur ut = (Utilisateur)dataContext.Utilisateurs.SingleOrDefault(Utilisateur => Utilisateur.Email == email);

            return ut;

        }


    }
}
