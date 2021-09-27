using AutoMapper;
using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    [Authorize]
    public class SancionesController : Controller
    {
        //Controlador con inyección de dependencias y patrón unit of work
        private readonly IUnitOfWork _unitOfWork;
        //Automapper
        private IMapper _mapper;
        //CONSTRUCTOR
        public SancionesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = MvcApplication.MapperConfiguration.CreateMapper(); //MvcApplication: clase del global.asax
        }

        // GET:
        public ActionResult Index()
        {
            //obtenemos listado de los prestamos vigentes
            var listEntidad = _unitOfWork.osanciones.GetList();
            //mapeamos la entidad a un DTO
            var listDTO = listEntidad.Select(x => _mapper.Map<SancionesDTO>(x)).ToList();
            //retornamos el modelo y la vista
            return View(listDTO);
        }

        //GET
        public ActionResult Details(Guid id)
        {
            //obtenemos el registro
            var entidad = _unitOfWork.osanciones.Get(id);
            //mapeamos
            var modelDTO = _mapper.Map<SancionesDTO>(entidad);
            return View(modelDTO);
        }
    }
}