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

        /// <summary>
        /// Método responsável por autenticar o usuário de acordo com os dados de entrada
        /// </summary>
        /// <param name="loginDTO">LoginDTO</param>
        /// <remarks>Autentica o usuário</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("login")]
        public IActionResult Logar([FromBody] LoginDTO loginDTO)
        {
            LoginValidation validation = new LoginValidation();
            ValidationResult validationResult = validation.Validate(loginDTO);

            if (!validationResult.IsValid)
            {
                return ResponseValidationResult(validationResult);
            }

            try
            {
                var usuario = _usuarioService.Logar(loginDTO.Email, loginDTO.Senha);

                if (usuario != null)
                {
                    var data = new
                    {
                        result = new
                        {
                            nome = usuario.Nome,
                            email = usuario.Email,
                        }
                    };

                    return Ok(new { success = true, data = data });
                }
                else
                {
                    var data = new
                    {
                        result = new
                        {
                            mensagem = "Usuário não encontrado!"
                        }
                    };

                    return NotFound(new { success = true, data = data });
                }
                }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Logar: {ex.Message}") });
            }
        }

        /// <summary>
        /// Método responsável por inserir um usuário
        /// </summary>
        /// <param name="usuarioDTO">UsuarioDTO</param>
        /// <remarks>Insere um usuário</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        public IActionResult Inserir([FromBody] UsuarioDTO usuarioDTO)
        {
            UsuarioValidation validation = new UsuarioValidation();
            ValidationResult validationResult = validation.Validate(usuarioDTO);

            if (!validationResult.IsValid)
            {
                return ResponseValidationResult(validationResult);
            }

            try
            {
                _usuarioService.Inserir(usuarioDTO);

                var data = new
                {
                    result = new
                    {
                        nome = usuarioDTO.Nome,
                        email = usuarioDTO.Email,
                    }
                };

                return Created(new Uri($"{Request.Path}/{usuarioDTO.Id}", UriKind.Relative), new { success = true, data = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Inserir: {ex.Message}") });
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
