using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;

namespace WebApplication.DesignPattern.Strategy
{
    public class UsuarioStrategy : IUsuarioStrategy
    {
        public void Add(UsuariosDTO modelDTO, IUnitOfWork unitOfWork)
        {
            var entidad = new Usuarios();
            entidad.usu_documento = modelDTO.usu_documento;
            entidad.usu_nombre = modelDTO.usu_nombre;
            entidad.usu_direccion = modelDTO.usu_direccion;
            entidad.usu_telefono = modelDTO.usu_telefono;
            entidad.usu_correo = modelDTO.usu_correo;
            entidad.usu_estado = "Activo";

            unitOfWork.ousuarios.Add(entidad);
            unitOfWork.Save();
        }
    }
}