using AutoMapper;
using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System;
using System.Collections.Generic;
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

        // GET: Usuarios
        public ActionResult Index()
        {
            Session.Clear();//limpia la variables de session que se crearan mas adelante
            var listEntidad = _unitOfWork.oprestamos.GetList().Where(x => x.pre_vigente == true);
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
                //pre_vigente viene por default false, lo dejamos así porque no se han asignado libros

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
            
            //utilizamos la variable de session creada anteriormente
            var modelPrestamosDTO = (PrestamosDTO)Session["PrestamosDTO"];

            try
            {                
                /*
                 Existen dos estrategias, una cuando se ha creado el préstamo y no se han asignado libros (pre_vigente=false)
                 y otra cuando se le asignan los libros (pre_vigente=true)
                */
                //en este punto consulta la entidad para saber mas que todo el estado del vigente
                var prestamo = _unitOfWork.oprestamos.Get(modelPrestamosDTO.pre_codigo);
                modelDTO.dtp_prestamo = prestamo.pre_codigo;

                var context = prestamo.pre_vigente == false ?
                                new PrestamoContext(new PrestamoNoVigenteStrategy()) :
                                new PrestamoContext(new PrestamoVigenteStrategy());

                context.Add(modelDTO, _unitOfWork);

                return RedirectToAction("Create");//con el view no entra el GetListLibros();
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
            var entidad = _unitOfWork.oprestamos.Get(id);
            var modelDTO = _mapper.Map<PrestamosDTO>(entidad);
            return View(modelDTO);
        }

        //GET
        public ActionResult ReturnBook()
        {
            //enviamos un modelo a la vista
            DetallePrestamosDTO modelDTO = new DetallePrestamosDTO();
            modelDTO.dtp_fecha_dev = DateTime.Now;
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

                var entidad = _unitOfWork.odetallePrestamos.GetList().Where(x => x.Prestamos.pre_usuario == modelDTO.Prestamos.pre_usuario && x.dtp_libro == modelDTO.dtp_libro).FirstOrDefault();
                entidad.dtp_fecha_dev = modelDTO.dtp_fecha_dev;

                _unitOfWork.odetallePrestamos.Update(entidad);
                _unitOfWork.Save();

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
        //GET AJAX        
        public ActionResult ValidationPartialView(string pre_usuario)
        {
            TempData["lista"] = GetListLibrosToReturn(pre_usuario);
            return PartialView("_ListReturnBooks");
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
        private void GetListUsuariosToReturn()
        {
            //obtenemos listado de prestamos
            var listPrestamos = _unitOfWork.oprestamos.GetList();
            //creamos la lista de usuarios que en detalle-prestamos no hallan devuelto el libro 
            List<UsuariosDTO> listUsuarios = new List<UsuariosDTO>();
            foreach (var item in listPrestamos)
            {
                foreach (var dtp in item.DetallePrestamos)
                {
                    //si dpt es null no tendrá dtp_fecha_dev, por tanto tampoco entrará
                    if (dtp.dtp_fecha_dev == null)
                    {
                        listUsuarios.Add(new UsuariosDTO() { usu_documento = item.pre_usuario, usu_nombre = item.Usuarios.usu_nombre });
                    }
                }
            }
            //quitamos los usuarios repetidos, el Distinc() no funciona porque compara solo valores y este un objeto entidad con varios campos
            //utilizamos entonces GroupBy() para agrupar c/elemento por su cantidad y escogemos el elemento 
            listUsuarios = listUsuarios.GroupBy(x => x.usu_documento).Select(x => x.FirstOrDefault()).ToList();
            ViewBag.listUsuarios = new SelectList(listUsuarios, "usu_documento", "usu_nombre");
        }
        private object GetListLibrosToReturn(string pre_usuario)
        {
            //obtenemos lista detalle-prestamos de un usuario
            var listDPT = _unitOfWork.odetallePrestamos.GetList().Where(x => x.Prestamos.pre_usuario == pre_usuario);
            //creamos la lista de libros que tiene ese usuario
            List<LibrosDTO> listLibros = new List<LibrosDTO>();
            foreach (var item in listDPT)
            {
                listLibros.Add(new LibrosDTO() { lib_codigo = item.dtp_libro, lib_nombre = item.Libros.lib_nombre });
            }
            //retornamos el objeto para poder asignarlo al TempData[]
            return new SelectList(listLibros, "lib_codigo", "lib_nombre");
        }
        #endregion
    }
}