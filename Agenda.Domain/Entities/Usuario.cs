using System;

namespace Agenda.Domain.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            var data = DateTime.Now;

            DataCadastro = data;
            DataAlteracao = data;
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string SenhaConfirmacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}
