using Agenda.Domain.DTOs;

namespace Agenda.Domain.Interfaces
{
    public interface IUsuarioService : IService<UsuarioDTO>
    {
        UsuarioDTO Logar(string email, string senha);
    }
}
