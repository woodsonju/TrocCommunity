using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    class QueryLivre : IQueryable<Livre>
    {
        public Expression Expression => throw new NotImplementedException();

        public Type ElementType => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public IEnumerator<Livre> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //public IQueryable<Livre> findByPoints(int min, int max)
        //{
        //    //return dataContext.Annonces.Include(book => book.Livre).AsNoTracking().Where(book => book.PointDuLivre >= min && book.PointDuLivre <= max);
        //}
    }
}
