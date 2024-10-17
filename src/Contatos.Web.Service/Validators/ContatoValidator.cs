using Contatos.Web.Domain.Entities;
using FluentValidation;

namespace Contatos.Web.Service.Validators;

public class ContatoValidator : AbstractValidator<Contato>
{
    public ContatoValidator() 
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Por favor informe o Nome.")
            .NotNull().WithMessage("Por favor informe o Nome.");

        RuleFor(x => x.Email)
                        .NotEmpty().WithMessage("Por favor informe o Email.")
                        .NotNull().WithMessage("Por favor informe o Email.");

        RuleFor(x => x.Telefone)
                        .NotEmpty().WithMessage("Por favor informe o Telefone.")
                        .NotNull().WithMessage("Por favor informe o Telefone.");
    }
}
