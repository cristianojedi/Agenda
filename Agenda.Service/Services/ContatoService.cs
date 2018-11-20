using System;
using System.Collections.Generic;
using Agenda.Domain.DTOs;
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;

namespace Agenda.Service.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public Guid Inserir(ContatoDTO objeto)
        {
            var contato = new Contato()
            {
                Id = new Guid(),
                Nome = objeto.Nome,
                Celular = objeto.Celular,
                Email = objeto.Email
            };

            return _contatoRepository.Inserir(contato);
        }

        public void Alterar(ContatoDTO objeto)
        {
            var contato = new Contato()
            {
                Id = objeto.Id,
                Nome = objeto.Nome,
                Celular = objeto.Celular,
                Email = objeto.Email,
                DataAlteracao = DateTime.Now
            };

            _contatoRepository.Alterar(contato);
        }

        public void Excluir(Guid id)
        {
            _contatoRepository.Excluir(id);
        }

        public ContatoDTO Consultar(Guid id)
        {
            var objeto = _contatoRepository.Consultar(id);

            if (objeto != null)
            {
                var contato = new ContatoDTO()
                {
                    Id = objeto.Id,
                    Nome = objeto.Nome,
                    Celular = objeto.Celular,
                    Email = objeto.Email
                };

                return contato;
            }

            return null;
        }

        public IList<ContatoDTO> Listar()
        {
            List<ContatoDTO> contatos = new List<ContatoDTO>();

            var objetos = _contatoRepository.Listar();

            if (objetos != null)
            {
                foreach (var item in objetos)
                {
                    var contato = new ContatoDTO()
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Celular = item.Celular,
                        Email = item.Email
                    };

                    contatos.Add(contato);
                }

                return contatos;
            }

            return null;
        }
    }
}
