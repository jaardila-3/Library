using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class prueba
    {
        public List<Usuarios> ListarUsuarios()
        {
            using (var db = new DBContext())
            {
                var lista = db.Usuarios.ToList();
                return lista;
            }
        }

    }
}
