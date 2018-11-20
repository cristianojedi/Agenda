using Agenda.Domain.DTOs;
using FluentValidation;
using System;

namespace Agenda.Service.Validations
{
    public class LoginValidation : AbstractValidator<LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure( x =>
                {
                    throw new ArgumentNullException("O objeto Login é nulo");
                });

            RuleFor(c => c.Email)
                .NotNull().WithMessage(Mensagens.EmailNotNull)
                .NotEmpty().WithMessage(Mensagens.EmailNotEmpty)
                .EmailAddress().WithMessage(Mensagens.EmailIvalido)
                .MinimumLength(6).WithMessage(Mensagens.EmailMinimumLength6)
                .MaximumLength(150).WithMessage(Mensagens.EmailMaximumLength150);

            RuleFor(c => c.Senha)
                .NotNull().WithMessage(Mensagens.SenhaNotNull)
                .NotEmpty().WithMessage(Mensagens.SenhaNotEmpty)
                .MinimumLength(6).WithMessage(Mensagens.SenhaMinimumLength6)
                .MaximumLength(20).WithMessage(Mensagens.SenhaMaximumLength20);
        }
    }
}
