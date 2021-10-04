using SimpleCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Data
{
    public interface IRepository<T> where T : BaseEntity    
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(int Id);
        T GetById(int Id);
        List<T> GetAll();
        List<T> FindIn(List<int> Idlist);
    }
}
