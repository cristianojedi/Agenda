using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using Agenda.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Infra.Data.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        public readonly AgendaContext _context;

        public ContatoRepository(AgendaContext agendaContext)
        {
            _context = agendaContext;
        }

        public Guid Inserir(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();

            return contato.Id;
        }

        public void Alterar(Contato contato)
        {
            _context.Attach(contato);
            _context.Entry(contato).Property("Nome").IsModified = true;
            _context.Entry(contato).Property("Celular").IsModified = true;
            _context.Entry(contato).Property("Email").IsModified = true;
            _context.Entry(contato).Property("DataAlteracao").IsModified = true;
            _context.SaveChanges();
        }

        public Contato Consultar(Guid id)
        {
            return _context.Contato.Find(id);
        }

        public IList<Contato> Listar()
        {
            return _context.Contato.ToList();
        }

        public void Excluir(Guid id)
        {
            var contato = Consultar(id);

            _context.Contato.Remove(contato);
            _context.SaveChanges();
        }
    }
}