using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Concrete.Repository.Entities;

namespace Sample.Concrete.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly SampleContext _context;
        private DbSet<T> _entities;

        public BaseRepository(SampleContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        
        public IEnumerable<T> Get()
        {
            return _entities.AsEnumerable();
        }

        public T Get(long id)
        {
            return _entities.SingleOrDefault(s => s.Id == id);
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            
            _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}