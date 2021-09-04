using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
