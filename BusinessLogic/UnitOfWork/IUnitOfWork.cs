using BusinessLogic.Repository;
using DataAccess.Models;

namespace BusinessLogic.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Usuarios> ousuarios { get; }
        IRepository<Libros> olibros { get; }
        IRepository<Areas> oareas { get; }
        IRepository<Prestamos> oprestamos { get; }
        IRepository<DetallePrestamos> odetallePrestamos { get; }
        IRepository<Sanciones> osanciones { get; }

        void Save();
    }
}
