using System;
using System.Collections.Generic;

namespace Agenda.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Inserir(T objeto);

        void Alterar(T objeto);

        void Excluir(Guid id);

        T Consultar(Guid id);

        IList<T> Listar();
    }
}
