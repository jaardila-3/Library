using BusinessLogic.Repository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Usuarios> ousuarios { get; }
        IRepository<Libros> olibros { get; }

        void Save();
    }
}
