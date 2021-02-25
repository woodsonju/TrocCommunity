using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal MyContext dataContext;
        internal DbSet<T> dbSet;


        public SQLRepository(MyContext DataContext)
        {
            this.dataContext = DataContext;
            dbSet = DataContext.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Delete(int id)
        {
            T t = FindById(id);
            if (dataContext.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);
            }
            dbSet.Remove(t);
        }

        public T FindById(int id)
        {
            return dbSet.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void SaveChanges()
        {
            dataContext.SaveChanges();

        }


        public void Update(T t)
        {
            dbSet.Attach(t);  //Charge l'objet t dans le contexte
            dataContext.Entry(t).State = EntityState.Modified;
        }

    }

}