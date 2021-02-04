using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;

namespace TrocCommunity.WebUi.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        List<T> items;
        string className;

        public MockContext()
        {
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(int id)
        {
            T ItemToDelete = items.Find(item => item.Id == id);
            if (ItemToDelete != null)
            {
                items.Remove(ItemToDelete);
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

        public T FindById(int id)
        {
            T t = items.Find(item => item.Id == id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void SaveChanges()
        {
            return;
        }

        public void Update(T t)
        {
            T ItemToUpdate = items.Find(item => item.Id == t.Id);
            if (ItemToUpdate != null)
            {
                ItemToUpdate = t;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }
    }
}
