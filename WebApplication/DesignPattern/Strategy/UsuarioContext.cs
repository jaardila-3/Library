using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;

namespace WebApplication.DesignPattern.Strategy
{
    public class UsuarioContext
    {
        private IUsuarioStrategy _strategy;
        
        public UsuarioContext(IUsuarioStrategy strategy)
        {
            _strategy = strategy;
        }
        public void Add(UsuariosDTO modelDTO, IUnitOfWork unitOfWork)
        {
            _strategy.Add(modelDTO, unitOfWork);
        }
    }
}