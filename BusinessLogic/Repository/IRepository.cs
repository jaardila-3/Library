using System;
using System.Collections.Generic;

namespace BusinessLogic.Repository
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetList();
        TEntity Get(int id);
        TEntity Get(string id);//para documento del usuario
        TEntity Get(Guid id);//para la tabla prestamos
        void Add(TEntity data);
        void Delete(int id);
        void Delete(string id); // para documento del usuario
        void Update(TEntity data);
        void Save();
    }
}
