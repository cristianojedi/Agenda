using Agenda.Domain.DTOs;
using FluentValidation;
using System;

namespace Agenda.Service.Validations
{
    public class UsuarioValidation : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidation()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure( x =>
                {
                    throw new ArgumentNullException("O objeto Usuario é nulo");
                });

            RuleFor(c => c.Nome)
                .NotNull().WithMessage(Mensagens.NomeNotNull)
                .NotEmpty().WithMessage(Mensagens.NomeNotEmpty)
                .MinimumLength(2).WithMessage(Mensagens.NomeMinimumLength2)
                .MaximumLength(50).WithMessage(Mensagens.NomeMaximumLength50);

            RuleFor(c => c.CPF)
                .NotNull().WithMessage(Mensagens.CPFNotNull)
                .NotEmpty().WithMessage(Mensagens.CPFNotIsEmpty)
                .Length(11).WithMessage(Mensagens.CPFLength)
                .Must(Utils.ValidarCpf).WithMessage(Mensagens.CPFInvalido);

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

            RuleFor(c => c.SenhaConfirmacao)
                .NotNull().WithMessage(Mensagens.SenhaConfirmacaoNotNull)
                .NotEmpty().WithMessage(Mensagens.SenhaConfirmacaoNotEmpty)
                .MinimumLength(6).WithMessage(Mensagens.SenhaConfirmacaoMinimumLength6)
                .MaximumLength(20).WithMessage(Mensagens.SenhaConfirmacaoMaximumLength20)
                .Must((x, senhaConfirmacao) => x.Senha.Equals(senhaConfirmacao, StringComparison.CurrentCulture))
                .WithMessage(Mensagens.SenhaConfirmacaoEqualsSenha);
        }
    }
}
