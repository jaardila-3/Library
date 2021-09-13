using BusinessLogic.UnitOfWork;
using WebApplication.Models.ViewModels;

namespace WebApplication.DesignPattern.Strategy
{
    public class UsuarioContext
    {
        private IUsuarioStrategy _strategy;
        //variable para cambiar la estrategia en ejecución
        //public IUsuarioStrategy strategy
        //{
        //    set
        //    {
        //        _strategy = value;
        //    }
        //}
        public UsuarioContext(IUsuarioStrategy strategy)
        {
            _strategy = strategy;
        }
        public void Add(FormUsuariosViewModel usuarioVM, IUnitOfWork unitOfWork)
        {
            _strategy.Add(usuarioVM, unitOfWork);
        }
    }
}