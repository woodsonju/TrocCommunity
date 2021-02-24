using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    public class SQLRepositoryLivre: SQLRepository<Livre>
    {
        public SQLRepositoryLivre(MyContext DataContext) : base(DataContext)
        {

        }




    }
}
