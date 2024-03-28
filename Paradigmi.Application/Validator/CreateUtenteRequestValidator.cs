using FluentValidation;
using Paradigmi.Application.Extensions;
using Paradigmi.Application.Models.Requests;

namespace Paradigmi.Application.Validator;

public class CreateUtenteRequestValidator : AbstractValidator<CreateUtenteRequest>
{
    public CreateUtenteRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Il campo email è obbligatorio")
            .NotNull()
            .WithMessage("Il campo email non può essere nullo")
            .RegEx(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                , "Il campo email non è nel formato corretto");


        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage("Il campo password è obbligatorio")
            .NotNull()
            .WithMessage("Il campo password non può essere nullo")
            .MinimumLength(6)
            .WithMessage("Il campo password deve essere almeno lungo 6 caratteri")
            .RegEx("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\-]).{6,}$"
                , "Il campo password deve essere lungo almeno 6 caratteri e deve contenere almeno un carattere maiuscolo, uno minuscolo, un numero e un carattere speciale"
            );
    }
}