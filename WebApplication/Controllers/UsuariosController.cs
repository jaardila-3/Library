using AutoMapper;
using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class UsuariosController : Controller
    {
        //Controlador con inyección de dependencias y patrón unit of work
        private readonly IUnitOfWork _unitOfWork;
        //Automapper
        private IMapper _mapper;
        //CONSTRUCTOR
        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = MvcApplication.MapperConfiguration.CreateMapper(); //MvcApplication: clase del global.asax
        }


        // GET: Usuarios
        public ActionResult Index()
        {
            var listEntidad = _unitOfWork.ousuarios.GetList();
            var listDTO = listEntidad.Select(x => _mapper.Map<UsuariosDTO>(x)).ToList();

            return View(listDTO);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(UsuariosDTO modelDTO)
        {
            if (!ModelState.IsValid)
            {                
                return View("Create", modelDTO);
            }   

            try
            {
                modelDTO.usu_estado = "Activo";
                var entidad = _mapper.Map<Usuarios>(modelDTO);

                _unitOfWork.ousuarios.Add(entidad);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                return View(modelDTO);
            }
        }

        // GET: /Edit/5
        public ActionResult Edit(string id)
        {
            //creamos el modelo para mostrarlo en la vista
            var entidad = _unitOfWork.ousuarios.Get(id);
            var modelDTO = _mapper.Map<UsuariosDTO>(entidad);
            
            return View(modelDTO);
        }

        // POST: /Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UsuariosDTO modelDTO)
        {
            try
            {
                modelDTO.usu_documento = id;
                modelDTO.usu_estado = "Activo";
                if (!ModelState.IsValid)
                {
                    return View("Create", modelDTO);
                }

                var entidad = _mapper.Map<Usuarios>(modelDTO);

                _unitOfWork.ousuarios.Update(entidad);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelDTO);
            }
        }

        // GET: /Delete/5
        public ActionResult Delete(string id)
        {
            var entidad = _unitOfWork.ousuarios.Get(id);
            var modelDTO = _mapper.Map<UsuariosDTO>(entidad);

            return View(modelDTO);
        }

        // POST: /Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                _unitOfWork.ousuarios.Delete(id);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var entidad = _unitOfWork.ousuarios.Get(id);
                var modelDTO = _mapper.Map<UsuariosDTO>(entidad);
                return View(modelDTO);
            }
        }
    }
}
