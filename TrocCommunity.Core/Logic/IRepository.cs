using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.Core.Logic
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Delete(int id);
        T FindById(int id);
        void Insert(T t);
        void SaveChanges();
        void Update(T t);
    }
}
