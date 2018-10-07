using System;
using Agenda.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;

namespace Agenda.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        T Post<V>(T objeto) where V : AbstractValidator<T>;

        T Put<V>(T objeto) where V : AbstractValidator<T>;

        void Delete(Guid id);

        T Get(Guid id);

        IList<T> Get();
    }
}
