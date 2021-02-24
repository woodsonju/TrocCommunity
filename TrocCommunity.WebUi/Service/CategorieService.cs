using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;

namespace TrocCommunity.WebUi.Service
{
    public class CategorieService : ICategorieService
    {
        private IRepository<Categorie> categoryRepository;

        public CategorieService(IRepository<Categorie> categoryRepository)
        {
/*            new SQLRepositoryUtilisateur(new MyContext());
*/
            this.categoryRepository = categoryRepository;
        }


        public void Insert(Categorie categorie)
        {
            categoryRepository.Insert(categorie);
            categoryRepository.SaveChanges();
        }
    }
}