using Agenda.Domain.DTOs;
using FluentValidation;
using System;

namespace Agenda.Service.Validations
{
    public class ContatoValidation : AbstractValidator<ContatoDTO>
    {
        public ContatoValidation()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure( x =>
                {
                    throw new ArgumentNullException("O objeto Contato é nulo");
                });

            RuleFor(c => c.Nome)
                .NotNull().WithMessage("Nome é obrigatório")
                .NotEmpty().WithMessage("Nome é obrigatório");

            RuleFor(c => c.Celular)
                .NotNull().WithMessage("Celular é obrigatório")
                .NotEmpty().WithMessage("Celular é obrigatório");

            RuleFor(c => c.Email)
                .NotNull().WithMessage("E-mail é obrigatório")
                .NotEmpty().WithMessage("E-mail é obrigatório");
        }

        private bool ValidarData(DateTime data)
        {
            return !data.Equals(default(DateTime));
        }
    }
}
