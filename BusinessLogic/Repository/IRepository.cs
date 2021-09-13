using System;
using System.Collections.Generic;

namespace BusinessLogic.Repository
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetList();
        TEntity GetForGuid(Guid id);
        TEntity GetForString(string id);//para la tabla documento
        void Add(TEntity data);
        void Delete(Guid id);
        void Update(TEntity data);
        void Save();
    }
}
