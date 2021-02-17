using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;
using System.Data.Entity;
namespace TrocCommunity.DataAccess.SQL.DAO
{
    class SQLRepositoryAnnonce : SQLRepository<Annonce>
    {
        public SQLRepositoryAnnonce(MyContext DataContext) : base(DataContext)
        {

        }

        //bare de Recherche

        public IQueryable<Annonce> findByKeyWord(string keyWord)
        {
            return dataContext.Annonces.Include(book => book.Livre).AsNoTracking().Where(book => book.Livre.Title.Contains(keyWord)
                                   || book.Livre.Author.Contains(keyWord)
                                   || book.Livre.Edition.Contains(keyWord)
                                   || book.Livre.Editor.Contains(keyWord));
        }

        public List<Annonce> findByPoints(int min,int max)
        {
            return dataContext.Annonces.Include(book => book.Livre).AsNoTracking().Where(book => book.PointDuLivre >= min && book.PointDuLivre <= max).ToList();

        }

        public List<Annonce> findByState(List<int> AcceptableState)
        {

            var request = (from a1 in dbSet where AcceptableState.Contains((int)a1.EtatDuLivre) select a1).ToList();
            return request;

        }

        public List<Annonce> findByDate(DateTime limitDate)
        {

            var request = (from a1 in dataContext.Annonces join book in dataContext.Livres on a1.LivreId equals book.Id where limitDate >a1.date select a1).ToList();
            return request;

        }

        //public List<Annonce> findByAllcriteria(string KeyWord,int min,int max, List<int> AcceptableState, DateTime limitDate)
        //{

        //    findByKeyWord()

        //}


    }


}
