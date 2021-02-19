using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;


namespace TrocCommunity.DataAccess.SQL.DAO
{

    public class SQLRepositoryLivre : SQLRepository<Livre>
    {

        

        public SQLRepositoryLivre(MyContext DataContext) : base(DataContext)
        {
            
        }



        public IEnumerable<Livre> Search(string search)
        {
            IEnumerable<Livre> book = dataContext.Livres.Where(p => search == null
            || p.Author.Contains(search));
            return book;
        }

        public IEnumerable<Livre> NbPagination(string search, int page, int pageSize)
        {
            var recherche = Search(search);
            var result = recherche.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }




        public IEnumerable<Livre> LivreParCategoriePagination(IEnumerable<Livre> book, string cat)
        {
            IEnumerable<Livre> result = dataContext.Livres.Where(x => x.Categorie.NomCategorie.Substring(0, 3) == cat);
            return result;

        }

        public IEnumerable<Livre> LivreParCategorie(string cat)
        {
            IEnumerable<Livre> result = dataContext.Livres.Where(x => x.Categorie.NomCategorie.Substring(0, 3) == cat);
            return result;

        }


    }
}
