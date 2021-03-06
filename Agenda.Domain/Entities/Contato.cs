﻿using System;

namespace Agenda.Domain.Entities
{
    public class Contato
    {
        public Contato()
        {
            var data = DateTime.Now;

            DataCadastro = data;
            DataAlteracao = data;
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}