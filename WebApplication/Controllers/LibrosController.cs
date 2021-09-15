using AutoMapper;
using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class LibrosController : Controller
    {
        //Controlador con inyección de dependencias y patrón unit of work
        private readonly IUnitOfWork _unitOfWork;
        //Automapper
        private IMapper _mapper;
        //CONSTRUCTOR
        public LibrosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = MvcApplication.MapperConfiguration.CreateMapper(); //MvcApplication: clase del global.asax
        }

        // GET: 
        public ActionResult Index()
        {
            var listEntidad = _unitOfWork.olibros.GetList();
            var listDTO = listEntidad.Select(x => _mapper.Map<LibrosDTO>(x)).ToList();
            
            return View(listDTO);
        }



        // GET: /Create
        public ActionResult Create()
        {            
            //EJEMPLO DE SELECTLIST DESDE C#
            var SelectList = _unitOfWork.oareas.GetList();
            ViewBag.SelectList = new SelectList(SelectList, "are_codigo", "are_nombre");
            return View();
        }

        //POST: /Create
        [HttpPost]
        public ActionResult Create(LibrosDTO modelDTO)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", modelDTO);
            }

            try
            {
                var entidad = _mapper.Map<Libros>(modelDTO);

                _unitOfWork.olibros.Add(entidad);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                return View(modelDTO);
            }
        }


        // GET: /Edit/5
        public ActionResult Edit(int id)
        {
            //creamos el modelo para mostrarlo en la vista
            var entidad = _unitOfWork.olibros.GetForInt(id);
            var modelDTO = _mapper.Map<LibrosDTO>(entidad);
            //EJEMPLO DE SELECTLIST DESDE C#
            var SelectList = _unitOfWork.oareas.GetList();
            ViewBag.SelectList = new SelectList(SelectList, "are_codigo", "are_nombre");
            return View(modelDTO);
        }

        // POST: /Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LibrosDTO modelDTO)
        {
            try
            {
                modelDTO.lib_codigo = id;
                var entidad = _mapper.Map<Libros>(modelDTO);

                _unitOfWork.olibros.Update(entidad);
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
        public ActionResult Delete(int id)
        {
            var entidad = _unitOfWork.olibros.GetForInt(id);
            var modelDTO = _mapper.Map<LibrosDTO>(entidad);

            return View(modelDTO);
        }

        // POST: /Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.olibros.Delete(id);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var entidad = _unitOfWork.olibros.GetForInt(id);
                var modelDTO = _mapper.Map<LibrosDTO>(entidad);
                return View(modelDTO);
            }
        }

    }
}
