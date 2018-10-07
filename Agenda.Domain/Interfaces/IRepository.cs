using System;
using System.Collections.Generic;
using Agenda.Domain.Entities;

namespace Agenda.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Inserir(T objeto);

        void Alterar(T objeto);

        void Deletar(Guid id);

        T Consultar(Guid id);

        IList<T> Listar();
    }
}
