using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class prueba
    {
        public List<areas> ListarAreas()
        {
            using (var db = new LibraryContext())
            {
                return db.Areas.ToList();
            }
        }

    }
}
