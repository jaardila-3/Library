using BusinessLogic;
using BusinessLogic.Repository;
using DataAccess.Models;
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

        //Controlador con inyeccion de dependencias
        private readonly IRepository<Usuarios> _repository;
        public PruebaController(IRepository<Usuarios> repository)
        {
            _repository = repository;
        }

        public ActionResult Listar()
        {
            IEnumerable<Usuarios> lista = _repository.GetList();
            return View(lista);
        }
    }
}