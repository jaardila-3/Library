using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;

namespace WebApplication.DesignPattern.Strategy.Prestamos
{
    public interface IPrestamoStrategy
    {
        void Add(DetallePrestamosDTO modelDTO, IUnitOfWork unitOfWork);

    }
}
