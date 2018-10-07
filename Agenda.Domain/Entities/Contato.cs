using System;

namespace Agenda.Domain.Entities
{
    public class Contato : BaseEntity
    {
        public string Nome
        {
            get;
            set;
        }
        public string Celular
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }

        public Contato()
        {
        }
    }
}
