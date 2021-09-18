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
            Session.Clear();//limpiara la variables de session que se crearan mas adelante
            var listEntidad = _unitOfWork.oprestamos.GetList();
            var listDTO = listEntidad.Select(x => _mapper.Map<PrestamosDTO>(x)).ToList();

            return View(listDTO);
        }

        // GET: /Create
        public ActionResult Create()
        {
            //enviamos un modelo a la vista
            PrestamosDTO modelDTO = new PrestamosDTO();
            modelDTO.pre_fecha = DateTime.Now;
            //si ya estamos trabajando con este controlador devolvemos el modelo existente
            if ((PrestamosDTO)Session["PrestamosDTO"] != null)
            {
                modelDTO = (PrestamosDTO)Session["PrestamosDTO"];
            }
            GetListUsuarios();//crea un viewbag con el selectList
            return View(modelDTO);
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
                modelDTO.pre_codigo = Guid.NewGuid();//creamos el guid para la tabla prestamos
                Session["PrestamosDTO"] = modelDTO; //diccionario de datos para utilizar en otros métodos
                var entidad = _mapper.Map<Prestamos>(modelDTO);

                _unitOfWork.oprestamos.Add(entidad);
                _unitOfWork.Save();

                GetListUsuarios();
                return View(modelDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                GetListUsuarios();
                return View(modelDTO);
            }
        }

        //POST
        public ActionResult AddBook(DetallePrestamosDTO modelDTO)
        {
            if (!ModelState.IsValid)
            {
                GetListLibros();
                return View("_AddLibros");
            }

            var modelPrestamosDTO = (PrestamosDTO)Session["PrestamosDTO"]; //utilizamos la variable de session creada anteriormente

            try
            {
                modelDTO.dtp_prestamo = modelPrestamosDTO.pre_codigo;//recuperamos el Guid de la tabla prestamos

                var entidad = _mapper.Map<DetallePrestamos>(modelDTO);

                _unitOfWork.odetallePrestamos.Add(entidad);
                _unitOfWork.Save();

                //TempData["modelPrestamosDTO"] = modelPrestamosDTO; //variable temporal de un método a otro
                return RedirectToAction("Create");//con el view no entra el GetListLibros();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                GetListUsuarios();//devolvemos la lista de usuarios para el select
                return View("Create", modelPrestamosDTO);
            }
        }

        #region MÉTODOS DE VISTAS PARCIALES
        //GET
        public ActionResult _AddLibros()
        {
            GetListLibros();
            return PartialView();
        }
        //GET
        public ActionResult _ListLibros()
        {
            var modelPrestamosDTO = (PrestamosDTO)Session["PrestamosDTO"]; //modelo creado en otro método
            Guid id = modelPrestamosDTO.pre_codigo;//el Guid de prestamos

            var listEntidad = _unitOfWork.odetallePrestamos.GetList();
            var filteredList = listEntidad.Where(x => x.dtp_prestamo == id).ToList();
            var listDTO = filteredList.Select(x => _mapper.Map<DetallePrestamosDTO>(x)).ToList();

            return PartialView(listDTO);
        }
        #endregion

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