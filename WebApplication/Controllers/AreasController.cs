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

        // GET: Usuarios
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



        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Usuarios/Create
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
            catch (Exception e)
            {
                return View();
            }
        }


        // GET: Areas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Areas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AreasDTO model)
        {
            try
            {
                var areasDB = new Areas();
                areasDB.are_codigo = id;
                areasDB.are_nombre = model.are_nombre;
                areasDB.are_tiempo = model.are_tiempo;

                _unitOfWork.oareas.Update(areasDB);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _unitOfWork.oareas.GetForInt(id);
            var area = new AreasDTO
            {
                are_codigo = model.are_codigo,
                are_nombre = model.are_nombre,
                are_tiempo = model.are_tiempo
            };

            return View(area);
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
            catch
            {
                return View();
            }
        }
    }
}
