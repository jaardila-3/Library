using AutoMapper;
using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.DesignPattern.Strategy.Prestamos;

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

        // GET:
        public ActionResult Index()
        {
            Session.Clear();//limpia la variables de session que se crearan mas adelante
            //obtenemos listado de los prestamos vigentes
            var listEntidad = _unitOfWork.oprestamos.GetList().Where(x => x.pre_vigente == true);
            //mapeamos la entidad a un DTO
            var listDTO = listEntidad.Select(x => _mapper.Map<PrestamosDTO>(x)).ToList();
            //retornamos el modelo y la vista
            return View(listDTO);
        }

        // GET: /Create
        public ActionResult Create()
        {
            //enviamos un modelo a la vista en caso de no haber un modelo existente
            PrestamosDTO modelDTO = new PrestamosDTO();
            modelDTO.pre_fecha = DateTime.Now;
            //si ya hay un modelo existente lo enviamos, recordar que trabajamos con variables de session mas adelante
            if ((PrestamosDTO)Session["PrestamosDTO"] != null) modelDTO = (PrestamosDTO)Session["PrestamosDTO"];
            //crea un viewbag con el selectList, HELPER
            GetListUsuarios();
            //retornamos el modelo y la vista
            return View(modelDTO);
        }

        //POST: /Create
        [HttpPost]
        public ActionResult Create(PrestamosDTO modelDTO)
        {
            try
            {
                //validamos el modelo
                if (!ModelState.IsValid)
                {
                    GetListUsuarios();
                    return View("Create", modelDTO);
                }
                //validamos si el usuario ya tiene prestamos
                var listPrestamos = _unitOfWork.oprestamos.GetList();
                var countVigente = listPrestamos.Where(x => x.pre_usuario == modelDTO.pre_usuario && x.pre_vigente == true).Count();
                if (countVigente > 0)
                {
                    ModelState.AddModelError("pre_usuario", "El Usuario tiene prestamos vigentes en el sistema");
                    GetListUsuarios();
                    return View("Create", modelDTO);
                }
                //creamos el guid para la tabla prestamos//pre_vigente viene por default false, lo dejamos así porque no se han asignado libros
                modelDTO.pre_codigo = Guid.NewGuid();
                //diccionario de datos para utilizar en varios métodos y vistas
                Session["PrestamosDTO"] = modelDTO;
                //mapeamos el modelo en una entidad
                var entidad = _mapper.Map<Prestamos>(modelDTO);
                //adicionamos y guardamos
                _unitOfWork.oprestamos.Add(entidad);
                _unitOfWork.Save();
                //HELPER y retornamos a la vista con el modelo
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
            //utilizamos la variable de session creada anteriormente
            var modelPrestamosDTO = (PrestamosDTO)Session["PrestamosDTO"];
            try
            {
                if (!ModelState.IsValid)
                {
                    GetListLibros();
                    return View("_AddLibros");
                }
                /*
                 Existen 4 estrategias, 1- cuando se ha creado el préstamo y no se han asignado libros (pre_vigente=false)
                 2- cuando se le asignan los libros (pre_vigente=true) y otras dos aplica para la devolución de libros
                */
                //en este punto consulta el registro para saber el estado del vigente
                var prestamo = _unitOfWork.oprestamos.Get(modelPrestamosDTO.pre_codigo);
                //asignamos el PK al modelDTO
                modelDTO.dtp_prestamo = prestamo.pre_codigo;
                //asignamos las estrategias al contexto
                var context = prestamo.pre_vigente == false ?
                                new PrestamoContext(new PrestamoNoVigenteStrategy()) :
                                new PrestamoContext(new PrestamoVigenteStrategy());
                //ejecutamos el contexto
                context.Add(modelDTO, _unitOfWork);
                //redirigimos a la acción para que tome el GetListLibros();
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                GetListUsuarios();//devolvemos la lista de usuarios para el select
                return View("Create", modelPrestamosDTO);
            }
        }

        //GET
        public ActionResult Details(Guid id)
        {
            //obtenemos el registro
            var entidad = _unitOfWork.oprestamos.Get(id);
            //mapeamos
            var modelDTO = _mapper.Map<PrestamosDTO>(entidad);
            return View(modelDTO);
        }

        //GET
        public ActionResult ReturnBook()
        {
            //enviamos un modelo a la vista con la fecha actual
            DetallePrestamosDTO modelDTO = new DetallePrestamosDTO();
            modelDTO.dtp_fecha_dev = DateTime.Now;
            //utilizamos HELPER de selectlist
            GetListUsuariosToReturn();
            return View(modelDTO);
        }

        // POST: Acción similar a EDIT
        [HttpPost]
        public ActionResult ReturnBook(DetallePrestamosDTO modelDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    GetListUsuariosToReturn();
                    return View(modelDTO);
                }
                //obtenemos la cantidad de registros donde el usuario tiene prestamos vigentes
                var countRegistros = _unitOfWork.odetallePrestamos.GetList().Where(x => x.Prestamos.pre_usuario == modelDTO.Prestamos.pre_usuario && x.dtp_fecha_dev == null).Count();
                //asignamos las estrategias, si tiene mas de un libro prestado solo afectara la fecha_dev, sino también afecta el vigente a false
                var context = countRegistros > 1 ?
                                new PrestamoContext(new PrestamoVigenteStrategy()) :
                                new PrestamoContext(new PrestamoNoVigenteStrategy());
                //ejecutamos el contexto
                context.Return(modelDTO, _unitOfWork);
                //redireccionamos al método o acción
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                GetListUsuariosToReturn();
                return View(modelDTO);
            }
        }

        #region MÉTODOS DE VISTAS PARCIALES
        //GET //utilizado en la vista CREATE
        public ActionResult _AddLibros()
        {
            GetListLibros();
            return PartialView();
        }
        //GET //utilizado en la vista CREATE
        public ActionResult _ListLibros()
        {
            var modelPrestamosDTO = (PrestamosDTO)Session["PrestamosDTO"]; //modelo creado en otro método
            Guid id = modelPrestamosDTO.pre_codigo;//el Guid de prestamos

            var listEntidad = _unitOfWork.odetallePrestamos.GetList();
            var filteredList = listEntidad.Where(x => x.dtp_prestamo == id).ToList();
            var listDTO = filteredList.Select(x => _mapper.Map<DetallePrestamosDTO>(x)).ToList();

            return PartialView(listDTO);
        }
        //GET //utilizado en la vista RETURNBOOK en el AJAX
        public ActionResult ValidationPartialView(string pre_usuario)
        {
            //obtenemos la lista de libros que el usuario tiene y la usamos en un TempData
            //para la vista parcial _ListReturnBooks usada en la vista ReturnBook
            TempData["lista"] = GetListLibrosToReturn(pre_usuario);
            return PartialView("_ListReturnBooks");
        }
        #endregion

        #region HELPERS
        //lista todos los usuarios del sistema
        private void GetListUsuarios()
        {
            //EJEMPLO DE SELECTLIST DESDE C#
            var listUsuarios = _unitOfWork.ousuarios.GetList();
            ViewBag.listUsuarios = new SelectList(listUsuarios, "usu_documento", "usu_nombre");
        }
        //lista todos los libros del sistema
        private void GetListLibros()
        {
            //EJEMPLO DE SELECTLIST DESDE C#
            var listLibros = _unitOfWork.olibros.GetList();
            ViewBag.listLibros = new SelectList(listLibros, "lib_codigo", "lib_nombre");
        }
        //lista los usuarios que tienen prestamos vigentes
        private void GetListUsuariosToReturn()
        {
            var list = _unitOfWork.oprestamos.GetList().Where(x => x.pre_vigente == true);
            ViewBag.listUsuarios = new SelectList(list, "pre_usuario", "Usuarios.usu_nombre");
        }
        //lista libros no devueltos de un usuario, usado en el método ValidationPartialView()
        private object GetListLibrosToReturn(string pre_usuario)
        {
            //obtenemos lista detalle-prestamos de un usuario
            var list = _unitOfWork.odetallePrestamos.GetList().Where(x => x.Prestamos.pre_usuario == pre_usuario && x.dtp_fecha_dev == null);
            return new SelectList(list, "dtp_libro", "Libros.lib_nombre");//retornamos objeto para asignarlo al TempData[]
        }
        #endregion
    }
}