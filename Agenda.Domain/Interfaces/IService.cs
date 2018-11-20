using System;
using System.Collections.Generic;

namespace Agenda.Domain.Interfaces
{
    public interface IService<T>
    {
        Guid Inserir(T objeto);

        void Alterar(T objeto);

        void Excluir(Guid id);

        T Consultar(Guid id);

        IList<T> Listar();
    }
}
