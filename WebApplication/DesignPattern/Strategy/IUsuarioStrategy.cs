using BusinessLogic.UnitOfWork;
using CommonComponents.DTOs;

namespace WebApplication.DesignPattern.Strategy
{
    public interface IUsuarioStrategy
    {
        void Add(UsuariosDTO usuariosDTO, IUnitOfWork unitOfWork);

    }
}
