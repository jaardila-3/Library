using BusinessLogic.UnitOfWork;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.DesignPattern.Strategy;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class UsuariosController : Controller
    {
        //Controlador con inyeccion de dependencias y patron unit of work
        private readonly IUnitOfWork _unitOfWork;
        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Usuarios
        public ActionResult Index()
        {
            IEnumerable<UsuariosViewModel> usuario = from d in _unitOfWork.ousuarios.GetList()
                                                     select new UsuariosViewModel
                                                     {
                                                         Documento = d.usu_documento,
                                                         Nombre = d.usu_nombre
                                                     };
            return View(usuario);
        }



        // GET: Usuarios/Create
        public ActionResult Create()
        {
            //EJEMPLO DE SELECTLIST DESDE C#
            //var SelectUsuarios = _unitOfWork.ousuarios.GetList();
            //ViewBag.SelectList = new SelectList(SelectUsuarios, "usu_documento", "usu_nombre");

            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(FormUsuariosViewModel usuarioVM)
        {
            if (!ModelState.IsValid)
            {
                //EJEMPLO DE SELECTLIST DESDE C#
                //var SelectUsuarios = _unitOfWork.ousuarios.GetList();
                //ViewBag.SelectList = new SelectList(SelectUsuarios, "usu_documento", "usu_nombre");
                return View("Create", usuarioVM);
            }   

            try
            {
                var context = new UsuarioContext(new UsuarioStrategy());
                context.Add(usuarioVM, _unitOfWork);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }





        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
