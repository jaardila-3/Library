using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;
using DataAccess.Models;
using System;

namespace WebApplication.DesignPattern.Strategy.Prestamos
{
    public class PrestamoNoVigenteStrategy : IPrestamoStrategy
    {
        public void Add(DetallePrestamosDTO modelDTO, IUnitOfWork unitOfWork)
        {
            var entidadDpt = new DetallePrestamos();
            entidadDpt.dtp_prestamo = modelDTO.dtp_prestamo;
            entidadDpt.dtp_libro = modelDTO.dtp_libro;
            entidadDpt.dtp_cantidad = modelDTO.dtp_cantidad;
            entidadDpt.dtp_fecha_fin = modelDTO.dtp_fecha_fin.Date + new TimeSpan(23, 59, 59);//la hora del día de entrega será las 23:59:59
            entidadDpt.dtp_fecha_dev = modelDTO.dtp_fecha_dev;

            var entidadPrestamo = unitOfWork.oprestamos.Get(modelDTO.dtp_prestamo);
            entidadPrestamo.pre_vigente = true;

            unitOfWork.odetallePrestamos.Add(entidadDpt);
            unitOfWork.oprestamos.Update(entidadPrestamo);
            unitOfWork.Save();

        }
    }
}