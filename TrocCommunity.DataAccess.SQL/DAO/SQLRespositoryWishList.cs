using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    public class SQLRepositoryWishList : SQLRepository<WishList>
    {
        public SQLRepositoryWishList(MyContext DataContext) : base(DataContext)
        {

        }


        public IQueryable<WishList> listWLbyIdClient(int idClient)
        {
            IQueryable<WishList> result = dataContext.WishLists.Where(x => x.ClientOwner == idClient);
            return result;
        }

        public IQueryable<WishList> listWLbyIdBook(int idClient)
        {
            IQueryable<WishList> result = dataContext.WishLists.Where(x => x.IdBook == idClient);
            return result;
        }


    }
}
