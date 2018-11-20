using Agenda.Domain.DTOs;
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Agenda.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Guid Inserir(UsuarioDTO objeto)
        {
            var usuario = new Usuario
            {
                Nome = objeto.Nome,
                CPF = objeto.CPF,
                Email = objeto.Email,
                Senha = objeto.Senha,
                SenhaConfirmacao = objeto.SenhaConfirmacao
            };

            return _usuarioRepository.Inserir(usuario);
        }

        public void Alterar(UsuarioDTO objeto)
        {
            var usuario = new Usuario
            {
                Id = objeto.Id,
                Nome = objeto.Nome,
                CPF = objeto.CPF,
                Email = objeto.Email,
                Senha = objeto.Senha,
                SenhaConfirmacao = objeto.SenhaConfirmacao,
                DataAlteracao = DateTime.Now
            };
        }

        public void Excluir(Guid id)
        {
            _usuarioRepository.Excluir(id);
        }

        public UsuarioDTO Consultar(Guid id)
        {
            var objeto = _usuarioRepository.Consultar(id);

            if (objeto != null)
            {
                var usuario = new UsuarioDTO()
                {
                    Id = objeto.Id,
                    Nome = objeto.Nome,
                    CPF = objeto.CPF,
                    Email = objeto.Email,
                    Senha = objeto.Senha,
                    SenhaConfirmacao = objeto.SenhaConfirmacao
                };

                return usuario;
            }

            return null;
        }

        public IList<UsuarioDTO> Listar()
        {
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();

            var objetos = _usuarioRepository.Listar();

            if (objetos != null)
            {
                foreach (var item in objetos)
                {
                    var usuario = new UsuarioDTO()
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        CPF = item.CPF,
                        Email = item.Email,
                    };

                    usuarios.Add(usuario);
                }

                return usuarios;
            }

            return null;
        }

        public UsuarioDTO Logar(string email, string senha)
        {
            var objeto = _usuarioRepository.Logar(email, senha);

            if (objeto != null)
            {
                var usuario = new UsuarioDTO()
                {
                    Id = objeto.Id,
                    Nome = objeto.Nome,
                    CPF = objeto.CPF,
                    Email = objeto.Email,
                    Senha = objeto.Senha,
                    SenhaConfirmacao = objeto.SenhaConfirmacao
                };

                return usuario;
            }

            return null;
        }
    }
}
