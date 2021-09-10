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
        public UnitOfWork(DBContext context)
        {
            _context = context;
        }

        //implementacion de la interfaz
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
