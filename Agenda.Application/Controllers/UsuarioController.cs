using System;
using System.Collections.Generic;
using Agenda.Domain.DTOs;
using Agenda.Domain.Interfaces;
using Agenda.Infra.CrossCutting.Models;
using Agenda.Service.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Consultar(Guid id)
        {
            try
            {
                var usuario = _usuarioService.Consultar(id);

                if (usuario != null)
                    return Ok(usuario);
                else
                    return NotFound("Usuário não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao executar o método Get: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] UsuarioDTO dto)
        {
            var listaErros = new List<KeyValuePair<string, string>>();
            UsuarioValidation validation = new UsuarioValidation();

            ValidationResult validationResult = validation.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var erro in validationResult.Errors)
                {
                    listaErros.Add(new KeyValuePair<string, string>(erro.PropertyName, erro.ErrorMessage));
                }

                return BadRequest(new { success = false, errors = listaErros });
            }

            try
            {
                _usuarioService.Inserir(dto);

                var data = new
                {
                    result = new
                    {
                        nome = dto.Nome,
                        email = dto.Email,
                    }
                };

                return Created(new Uri($"{Request.Path}/{dto.Id}", UriKind.Relative), new { success = true, data = data });
            }
            catch (Exception ex)
            {
                listaErros.Add(new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Inserir: {ex.Message}"));
                return BadRequest(new { success = false, errors = listaErros });
            }
        }

        [HttpPut]
        public IActionResult Alterar([FromBody] UsuarioDTO dto)
        {
            var listaErros = new List<KeyValuePair<string, string>>();
            UsuarioValidation validation = new UsuarioValidation();
            ValidationResult result = validation.Validate(dto);

            if (!result.IsValid)
            {
                foreach (var erro in result.Errors)
                {
                    listaErros.Add(new KeyValuePair<string, string>(erro.PropertyName, erro.ErrorMessage));
                }

                return BadRequest(new { success = false, errors = listaErros });
            }

            try
            {
                _usuarioService.Alterar(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                listaErros.Add(new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Alterar: {ex.Message}"));
                return BadRequest(new { success = false, errors = listaErros });
            }
        }

        [HttpDelete]
        public IActionResult Excluir(Guid id)
        {
            var listaErros = new List<KeyValuePair<string, string>>();

            try
            {
                _usuarioService.Excluir(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                listaErros.Add(new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Excluir: {ex.Message}"));
                return BadRequest(new { success = false, errors = listaErros });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var listaErros = new List<KeyValuePair<string, string>>();

            try
            {
                var usuarios = _usuarioService.Listar();

                if (usuarios != null)
                    return Ok(usuarios);
                else
                    return NotFound("Não existem Usuários cadastrados");
            }
            catch (Exception ex)
            {
                listaErros.Add(new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Excluir: {ex.Message}"));
                return BadRequest(new { success = false, errors = listaErros });
            }
        }
    }
}