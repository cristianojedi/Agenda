using System;
using System.Collections.Generic;
using System.Linq;
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
                {
                    var data = new
                    {
                        success = true,
                        result = new
                        {
                            nome = usuario.Nome,
                            email = usuario.Email,
                        }
                    };

                    return Ok(data);
                }
                else
                {
                    var data = new
                    {
                        success = true,
                        result = new
                        {
                            mensagem = "Usuário não encontrado!"
                        }
                    };

                    return NotFound(data);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Consultar: {ex.Message}") });
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Logar([FromBody] LoginDTO dto)
        {
            LoginValidation validation = new LoginValidation();
            ValidationResult validationResult = validation.Validate(dto);

            if (!validationResult.IsValid)
            {
                return ResponseValidationResult(validationResult);
            }

            try
            {
                var usuario = _usuarioService.Logar(dto.Email, dto.Senha);

                var data = new
                {
                    success = true,
                    result = new
                    {
                        nome = usuario.Nome,
                        email = usuario.Email,
                    }
                };

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Logar: {ex.Message}") });
            }
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] UsuarioDTO dto)
        {
            UsuarioValidation validation = new UsuarioValidation();
            ValidationResult validationResult = validation.Validate(dto);

            if (!validationResult.IsValid)
            {
                return ResponseValidationResult(validationResult);
            }

            try
            {
                _usuarioService.Inserir(dto);

                var data = new
                {
                    success = true,
                    result = new
                    {
                        nome = dto.Nome,
                        email = dto.Email,
                    }
                };

                return Created(new Uri($"{Request.Path}/{dto.Id}", UriKind.Relative), data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Inserir: {ex.Message}") });
            }
        }

        [HttpPut]
        public IActionResult Alterar([FromBody] UsuarioDTO dto)
        {
            var listaErros = new List<KeyValuePair<string, string>>();
            UsuarioValidation validation = new UsuarioValidation();
            ValidationResult validationResult = validation.Validate(dto);

            if (!validationResult.IsValid)
            {
                return ResponseValidationResult(validationResult);
            }

            try
            {
                var data = new
                {
                    success = true,
                    result = new
                    {
                        nome = dto.Nome,
                        email = dto.Email,
                    }
                };

                _usuarioService.Alterar(dto);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Alterar: {ex.Message}") });
            }
        }

        [HttpDelete]
        public IActionResult Excluir(Guid id)
        {
            try
            {
                _usuarioService.Excluir(id);
                return NotFound(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Excluir: {ex.Message}") });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
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
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Listar: {ex.Message}") });
            }
        }

        private IActionResult ResponseValidationResult(ValidationResult validationResult)
        {
            var listaErros = new List<KeyValuePair<string, string>>();

            foreach (var erro in validationResult.Errors)
            {
                listaErros.Add(new KeyValuePair<string, string>(erro.PropertyName, erro.ErrorMessage));
            }

            return BadRequest(new { success = false, errors = listaErros });
        }
    }
}
