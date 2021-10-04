using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Context;
using SimpleCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly LibraryContext _context;
        protected readonly DbSet<T> dataset;
        public Repository(LibraryContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        public T Add(T entity)
        {
            dataset.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            var result = dataset.SingleOrDefault(result => result.Id.Equals(entity.Id));

            if (result == null)
            {
                return null;
            }
            _context.Entry(result).CurrentValues.SetValues(entity);
            _context.SaveChanges();

            return entity;
        }
        public T GetById(int Id)
        {
            return dataset.SingleOrDefault(result => result.Id.Equals(Id));
        }
        public List<T> GetAll()
        {
           
            return dataset.ToList();
        }
        public List<T> FindIn(List<int> Idlist)
        {
            return dataset.Where(data => Idlist.Contains(data.Id)).ToList();
        }
        public void Delete(int Id)
        {
            var result = dataset.SingleOrDefault(result => result.Id.Equals(Id));
            if (result != null)
            {
                dataset.Remove(result);
                _context.SaveChanges();
            }
        }
      
    }

}

