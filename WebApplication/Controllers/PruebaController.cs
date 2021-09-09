using BusinessLogic;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PruebaController : Controller
    {           
        // GET: Prueba
        public ActionResult Index()
        {
           var lista = new prueba().ListarUsuarios();
           return View(lista);
        }
    }
}