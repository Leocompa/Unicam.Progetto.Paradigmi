using FluentValidation;
using Paradigmi.Application.Extensions;
using Paradigmi.Application.Models.Requests;

namespace Paradigmi.Application.Validator;

public class CreateTokenRequestValidator : AbstractValidator<CreateTokenRequest>
{
    public CreateTokenRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Il campo email è obbligatorio")
            .NotNull()
            .WithMessage("Il campo email non può essere nullo")
            .RegEx(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                , "Il campo email non è nel formato corretto");
        
    }
}