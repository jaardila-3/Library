using BusinessLogic.UnitOfWork;
using WebApplication.Models.ViewModels;

namespace WebApplication.DesignPattern.Strategy
{
    public interface IUsuarioStrategy
    {
        void Add(FormUsuariosViewModel usuarioVM, IUnitOfWork unitOfWork);

    }
}
