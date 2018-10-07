using System;

namespace Agenda.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id
        {
            get;
            set;
        }

        public virtual DateTime DataCadastro
        {
            get;
            set;
        }

        public virtual DateTime DataAlteracao
        {
            get;
            set;
        }
    }
}
