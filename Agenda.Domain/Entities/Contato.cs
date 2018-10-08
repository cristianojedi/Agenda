using System;

namespace Agenda.Domain.Entities
{
    public class Contato
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

        public Guid Id
        {
            get;
            set;
        }

        public DateTime DataCadastro
        {
            get;
            set;
        }

        public DateTime DataAlteracao
        {
            get;
            set;
        }

        public Contato()
        {
            var data = DateTime.Now;

            DataCadastro = data;
            DataAlteracao = data;
        }
    }
}