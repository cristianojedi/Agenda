using System;

namespace Agenda.Domain.DTOs
{
    public class ContatoDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }
    }
}
