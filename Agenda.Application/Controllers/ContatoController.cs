using System;
using System.Collections.Generic;
using Agenda.Domain.DTOs;
using Agenda.Domain.Interfaces;
using Agenda.Service.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        /// <summary>
        /// Método responsável por consultar um contato pelo seu id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <remarks>Consulta um contato</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Consultar(Guid id)
        {
            try
            {
                var contato = _contatoService.Consultar(id);

                if (contato != null)
                {
                    var data = new
                    {
                        success = true,
                        result = new
                        {
                            id = contato.Id,
                            nome = contato.Nome,
                            celular = contato.Celular,
                            email = contato.Email
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
                            mensagem = "Contato não encontrado!"
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

        /// <summary>
        /// Método responsável por inserir um contato
        /// </summary>
        /// <param name="contatoDTO">ContatoDTO</param>
        /// <remarks>Insere um contato</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        public IActionResult Inserir([FromBody] ContatoDTO contatoDTO)
        {
            ContatoValidation validation = new ContatoValidation();
            ValidationResult validationResult = validation.Validate(contatoDTO);

            if (!validationResult.IsValid)
            {
                return ResponseValidationResult(validationResult);
            }

            try
            {
                _contatoService.Inserir(contatoDTO);

                var data = new
                {
                    success = true,
                    result = new
                    {
                        id = contatoDTO.Id,
                        nome = contatoDTO.Nome,
                        celular = contatoDTO.Celular,
                        email = contatoDTO.Email
                    }
                };

                return Created(new Uri($"{Request.Path}/{contatoDTO.Id}", UriKind.Relative), data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Inserir: {ex.Message}") });
            }
        }

        /// <summary>
        /// Método responsável por alterar um contato
        /// </summary>
        /// <param name="contatoDTO">ContatoDTO</param>
        /// <remarks>Altera um contato</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        public IActionResult Alterar([FromBody] ContatoDTO contatoDTO)
        {
            ContatoValidation validation = new ContatoValidation();
            ValidationResult validationResult = validation.Validate(contatoDTO);

            if (!validationResult.IsValid)
            {
                return ResponseValidationResult(validationResult);
            }

            try
            {
                _contatoService.Alterar(contatoDTO);

                var data = new
                {
                    success = true,
                    result = new
                    {
                        id = contatoDTO.Id,
                        nome = contatoDTO.Nome,
                        celular = contatoDTO.Celular,
                        email = contatoDTO.Email
                    }
                };

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Alterar: {ex.Message}") });
            }
        }

        /// <summary>
        /// Método responsável por deletar um contato
        /// </summary>
        /// <param name="id">Guid</param>
        /// <remarks>Deleta um contato</remarks>
        /// <response code="404">NotFound</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        public IActionResult Excluir(Guid id)
        {
            try
            {
                _contatoService.Excluir(id);

                var data = new
                {
                    success = true,
                    result = new
                    {
                        id = id
                    }
                };

                return NotFound(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, errors = new KeyValuePair<string, string>("BadRequest", $"Erro ao executar o método Excluir: {ex.Message}") });
            }
        }

        /// <summary>
        /// Método responsável por listar todos os contatos
        /// </summary>
        /// <remarks>Lista todos os contatos</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var contatos = _contatoService.Listar();

                if (contatos != null)
                    return Ok(contatos);
                else
                    return NotFound("Agenda vazia!");
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