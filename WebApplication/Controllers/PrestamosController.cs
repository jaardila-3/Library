using AutoMapper;
using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PrestamosController : Controller
    {
        //Controlador con inyección de dependencias y patrón unit of work
        private readonly IUnitOfWork _unitOfWork;
        //Automapper
        private IMapper _mapper;
        //CONSTRUCTOR
        public PrestamosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = MvcApplication.MapperConfiguration.CreateMapper(); //MvcApplication: clase del global.asax
        }


        // GET: Usuarios
        public ActionResult Index()
        {
            var listEntidad = _unitOfWork.oprestamos.GetList();
            var listDTO = listEntidad.Select(x => _mapper.Map<PrestamosDTO>(x)).ToList();

            return View(listDTO);
        }

        // GET: /Create
        public ActionResult Create()
        {
            GetListUsuarios();
            return View();
        }

        //POST: /Create
        [HttpPost]
        public ActionResult Create(PrestamosDTO modelDTO)
        {
            if (!ModelState.IsValid)
            {
                GetListUsuarios();
                return View("Create", modelDTO);
            }

            try
            {
                var entidad = _mapper.Map<Prestamos>(modelDTO);

                _unitOfWork.oprestamos.Add(entidad);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                GetListUsuarios();
                return View(modelDTO);
            }
        }


        #region HELPERS
        private void GetListUsuarios()
        {
            //EJEMPLO DE SELECTLIST DESDE C#
            var listUsuarios = _unitOfWork.ousuarios.GetList();
            ViewBag.listUsuarios = new SelectList(listUsuarios, "usu_documento", "usu_nombre");
        }
        private void GetListLibros()
        {
            //EJEMPLO DE SELECTLIST DESDE C#
            var listLibros = _unitOfWork.olibros.GetList();
            ViewBag.listLibros = new SelectList(listLibros, "lib_codigo", "lib_nombre");
        }
        #endregion
    }
}