using AutoMapper;
using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class AreasController : Controller
    {
        //Controlador con inyección de dependencias y patrón unit of work
        private readonly IUnitOfWork _unitOfWork;
        //Automapper
        private IMapper _mapper;
        //CONSTRUCTOR
        public AreasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = MvcApplication.MapperConfiguration.CreateMapper(); //MvcApplication: clase del global.asax
        }        

        // GET: 
        public ActionResult Index()
        {
            var listAreas = _unitOfWork.oareas.GetList();
            var listAreasDTO = listAreas.Select(x => _mapper.Map<AreasDTO>(x)).ToList();

            //IEnumerable<AreasDTO> listAreasDTO = from d in _unitOfWork.oareas.GetList()
            //                              select new AreasDTO
            //                              {
            //                                  are_codigo = d.are_codigo,
            //                                  are_nombre = d.are_nombre,
            //                                  are_tiempo = d.are_tiempo
            //                              };
            return View(listAreasDTO);
        }



        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: /Create
        [HttpPost]
        public ActionResult Create(AreasDTO modelDTO)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", modelDTO);
            }

            try
            {
                var entidad = _mapper.Map<Areas>(modelDTO);

                //var entidad = new Areas();
                //entidad.are_codigo = modelDTO.are_codigo;
                //entidad.are_nombre = modelDTO.are_nombre;
                //entidad.are_tiempo = modelDTO.are_tiempo;

                _unitOfWork.oareas.Add(entidad);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                return View(modelDTO);
            }
        }


        // GET: Areas/Edit/5
        public ActionResult Edit(int id)
        {
            //creamos el modelo para mostrarlo en la vista
            var entidad = _unitOfWork.oareas.Get(id);
            var modelDTO = _mapper.Map<AreasDTO>(entidad);
            return View(modelDTO);
        }

        // POST: Areas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AreasDTO modelDTO)
        {
            try
            {
                modelDTO.are_codigo = id;
                if (!ModelState.IsValid)
                {
                    return View("Create", modelDTO);
                }

                var entidad = _mapper.Map<Areas>(modelDTO);                

                _unitOfWork.oareas.Update(entidad);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);                
                return View(modelDTO);
            }
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int id)
        {
            var entidad = _unitOfWork.oareas.Get(id);
            var modelDTO = _mapper.Map<AreasDTO>(entidad);
            
            return View(modelDTO);
        }

        // POST: Areas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.oareas.Delete(id);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var entidad = _unitOfWork.oareas.Get(id);
                var modelDTO = _mapper.Map<AreasDTO>(entidad);
                return View(modelDTO);
            }
        }
    }
}
