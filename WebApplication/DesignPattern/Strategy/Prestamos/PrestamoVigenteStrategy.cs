using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System.Linq;

namespace WebApplication.DesignPattern.Strategy.Prestamos
{
    public class PrestamoVigenteStrategy : IPrestamoStrategy
    {
        public void Add(DetallePrestamosDTO modelDTO, IUnitOfWork unitOfWork)
        {
            var entidad = new DetallePrestamos();
            entidad.dtp_prestamo = modelDTO.dtp_prestamo;
            entidad.dtp_libro = modelDTO.dtp_libro;
            entidad.dtp_cantidad = modelDTO.dtp_cantidad;
            entidad.dtp_fecha_fin = modelDTO.dtp_fecha_fin;//.Date + new TimeSpan(23, 59, 59);//la hora del día de entrega será las 23:59:59
            entidad.dtp_fecha_dev = modelDTO.dtp_fecha_dev;

            unitOfWork.odetallePrestamos.Add(entidad);
            unitOfWork.Save();          

        }

        public void Return(DetallePrestamosDTO modelDTO, IUnitOfWork unitOfWork)
        {
            var entidad = unitOfWork.odetallePrestamos.GetList().Where(x => x.Prestamos.pre_usuario == modelDTO.Prestamos.pre_usuario && x.dtp_libro == modelDTO.dtp_libro).FirstOrDefault();
            entidad.dtp_fecha_dev = modelDTO.dtp_fecha_dev;

            unitOfWork.odetallePrestamos.Update(entidad);
            unitOfWork.Save();
        }
    }
}