using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
        public int Count(string cat = null)
        {

            if (cat == null)
            {
                return dataContext.Livres.AsNoTracking().Count();
            }
            else
            {
                return dataContext.Livres.AsNoTracking().Where(x => x.Categorie.NomCategorie.Substring(0, 3) == cat).Count();

            }

        }
        public int SearchCount(string search)
        {

            return dataContext.Livres.AsNoTracking().Where(p => search == null
            || p.Author.Contains(search)).Count();

        }

        public IEnumerable<Livre> Search(string search)
        {
            IEnumerable<Livre> book = new List<Livre>();

                
           book = dataContext.Livres.AsNoTracking().Where(p => search == null
            || p.Author.Contains(search));
            
            return book;
        }

        public List<Livre> NbPaginationSearch(int page, int pageSize, string search)
        {

            return Search(search).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Livre> NbPagination(int page, int pageSize,string cat)
        {
            IEnumerable<Livre> result = new List<Livre>();

            if (cat == null)
            {
                result = dataContext.Livres.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                result = LivreParCategorie(cat).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            }
            

            return result.ToList();
        }


        public IQueryable<Livre> LivreParCategorie(string cat)
        {
            IQueryable<Livre> result = dataContext.Livres.Where(x => x.Categorie.NomCategorie.Substring(0, 3) == cat);
            return result;

        }


    }
}
