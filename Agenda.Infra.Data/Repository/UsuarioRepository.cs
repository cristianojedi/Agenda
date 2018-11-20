using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using Agenda.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly AgendaContext _context;

        public UsuarioRepository(AgendaContext context)
        {
            _context = context;
        }

        public void Inserir(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public void Alterar(Usuario usuario)
        {
            _context.Attach(usuario);
            _context.Entry(usuario).Property("Nome").IsModified = true;
            _context.Entry(usuario).Property("CPF").IsModified = true;
            _context.Entry(usuario).Property("Email").IsModified = true;
            _context.Entry(usuario).Property("Senha").IsModified = true;
            _context.Entry(usuario).Property("SenhaConfirmacao").IsModified = true;
            _context.Entry(usuario).Property("DataAlteracao").IsModified = true;
            _context.SaveChanges();
        }

        public Usuario Consultar(Guid id)
        {
            return _context.Usuario.Find(id);
        }

        public IList<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public void Excluir(Guid id)
        {
            var usuario = Consultar(id);

            _context.Remove(usuario);
            _context.SaveChanges();
        }

        public Usuario Logar(string email, string senha)
        {
            var usuario = (from usu in _context.Usuario
                          where usu.Email == email && usu.Senha == senha
                          select usu).SingleOrDefault();

            return usuario;
        }
    }
}
