using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetList();
        TEntity Get(Guid id);
        void Add(TEntity data);
        void Delete(Guid id);
        void Update(TEntity data);
        void Save();
    }
}
