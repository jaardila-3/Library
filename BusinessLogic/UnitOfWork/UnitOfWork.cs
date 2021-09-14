using BusinessLogic.Repository;
using DataAccess.Models;

namespace BusinessLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DBContext _context;
        //variables para guardar el objetos si existe
        private IRepository<Usuarios> _usuario;
        private IRepository<Libros> _libro;
        private IRepository<Areas> _areas;
        public UnitOfWork(DBContext context)
        {
            _context = context;
        }

        //implementación de la interfaz
        public IRepository<Usuarios> ousuarios
        {
            get
            {
                return _usuario == null ?
                    _usuario = new Repository<Usuarios>(_context) :
                    _usuario;
            }
        }

        public IRepository<Libros> olibros
        {
            get
            {
                return _libro == null ?
                    _libro = new Repository<Libros>(_context) :
                    _libro;
            }
        }

        public IRepository<Areas> oareas
        {
            get
            {
                return _areas == null ?
                    _areas = new Repository<Areas>(_context) :
                    _areas;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
