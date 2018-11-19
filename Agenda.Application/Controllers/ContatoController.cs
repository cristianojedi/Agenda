using System;
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

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Consultar(Guid id)
        {
            try
            {
                var contato = _contatoService.Consultar(id);

                if (contato != null)
                    return Ok(contato);
                else
                    return NotFound("Contato não encontrado na Agenda!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao executar o método Get: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] ContatoDTO dto)
        {
            ContatoValidation validation = new ContatoValidation();
            ValidationResult result = validation.Validate(dto);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            try
            {
                _contatoService.Inserir(dto);
                return Created(new Uri($"{Request.Path}/{dto.Id}", UriKind.Relative), dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao executar o método Inserir: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult Alterar([FromBody] ContatoDTO dto)
        {
            ContatoValidation validation = new ContatoValidation();
            ValidationResult result = validation.Validate(dto);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            try
            {
                _contatoService.Alterar(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao executar o método Alterar: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Excluir(Guid id)
        {
            try
            {
                _contatoService.Excluir(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao executar o método Excluir: {ex.Message}");
            }
        }

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
                return BadRequest($"Erro ao executar o método Listar: {ex.Message}");
            }
        }
    }
}