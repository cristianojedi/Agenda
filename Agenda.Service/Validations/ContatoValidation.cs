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
                .NotNull().WithMessage(Mensagens.NomeNotNull)
                .NotEmpty().WithMessage(Mensagens.NomeNotEmpty)
                .MinimumLength(2).WithMessage(Mensagens.NomeMinimumLength2)
                .MaximumLength(100).WithMessage(Mensagens.NomeMaximumLength100);

            RuleFor(c => c.Celular)
                .NotNull().WithMessage(Mensagens.CelularNotNull)
                .NotEmpty().WithMessage(Mensagens.CelularNotEmpty)
                .Length(11).WithMessage(Mensagens.CelularLength);

            RuleFor(c => c.Email)
                .NotNull().WithMessage(Mensagens.EmailNotNull)
                .NotEmpty().WithMessage(Mensagens.EmailNotEmpty)
                .EmailAddress().WithMessage(Mensagens.EmailIvalido)
                .MinimumLength(6).WithMessage(Mensagens.EmailMinimumLength6)
                .MaximumLength(150).WithMessage(Mensagens.EmailMaximumLength150);
        }
    }
}
