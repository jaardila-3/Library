using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessLogic.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DBContext _context;
        private DbSet<TEntity> _dbSet;

        public Repository(DBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity data) => _dbSet.Add(data);
        

        public void Delete(Guid id)
        {
            var dataToDelete = _dbSet.Find(id);
            _dbSet.Remove(dataToDelete);
        }

        public TEntity Get(Guid id) => _dbSet.Find(id);
        

        public IEnumerable<TEntity> GetList() => _dbSet.ToList();


        public void Save() => _context.SaveChanges();
        

        public void Update(TEntity data)
        {
            _dbSet.Attach(data);//attach hace el cambio del elemento, lo sobreescribe
            _context.Entry(data).State = EntityState.Modified;
        }
    }
}
