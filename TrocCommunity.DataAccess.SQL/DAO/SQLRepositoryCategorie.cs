using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    public class SQLRepositoryCategorie : SQLRepository<Categorie>
    {

        public SQLRepositoryCategorie(MyContext DataContext) : base(DataContext)
        {

        }

        public Categorie FindByName(string name)
        {
            return dataContext.Categories.SingleOrDefault(cat => cat.NomCategorie == name);
        }

        

    }
}
