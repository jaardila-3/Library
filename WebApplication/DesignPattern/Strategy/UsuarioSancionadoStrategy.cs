using BusinessLogic.UnitOfWork;
using DataAccess.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.DesignPattern.Strategy
{
    public class UsuarioSancionadoStrategy : IUsuarioStrategy
    {
        public void Add(FormUsuariosViewModel usuarioVM, IUnitOfWork unitOfWork)
        {
            var usuarioDB = new Usuarios();
            usuarioDB.usu_documento = usuarioVM.Documento;
            usuarioDB.usu_nombre = usuarioVM.Nombre;
            usuarioDB.usu_direccion = usuarioVM.Direccion;
            usuarioDB.usu_telefono = usuarioVM.Telefono;
            usuarioDB.usu_correo = usuarioVM.Correo;
            usuarioDB.usu_estado = "Sancionado";

            unitOfWork.ousuarios.Add(usuarioDB);
            unitOfWork.Save();
        }
    }
}