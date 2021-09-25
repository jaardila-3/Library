using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;

namespace WebApplication.DesignPattern.Strategy.Prestamos
{
    public class PrestamoContext
    {
        private IPrestamoStrategy _strategy;
        
        public PrestamoContext(IPrestamoStrategy strategy)
        {
            _strategy = strategy;
        }
        public void Add(DetallePrestamosDTO modelDTO, IUnitOfWork unitOfWork)
        {
            _strategy.Add(modelDTO, unitOfWork);
        }
        public void Return(DetallePrestamosDTO modelDTO, IUnitOfWork unitOfWork)
        {
            _strategy.Return(modelDTO, unitOfWork);
        }
    }
}