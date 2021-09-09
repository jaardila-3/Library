using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class prueba
    {
        public List<usuarios> ListarUsuarios()
        {
            using (var db = new LibraryContext())
            {
                var lista = db.Usuarios.ToList();
                return lista;
            }
        }

    }
}
