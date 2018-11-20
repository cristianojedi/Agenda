using Agenda.Domain.Entities;

namespace Agenda.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Logar(string email, string senha);
    }
}
