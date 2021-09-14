using BusinessLogic.Repository;
using DataAccess.Models;

namespace BusinessLogic.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Usuarios> ousuarios { get; }
        IRepository<Libros> olibros { get; }
        IRepository<Areas> oareas { get; }

        void Save();
    }
}
