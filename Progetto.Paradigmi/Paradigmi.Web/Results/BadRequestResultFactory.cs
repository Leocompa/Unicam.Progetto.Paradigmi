using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Paradigmi.Application.Responses;

namespace Paradigmi.Web.Results;

public class BadRequestResultFactory : BadRequestObjectResult
{
    public BadRequestResultFactory(ActionContext context) : base(new BadResponse())
    {
        var retErrors = new List<string>();
        foreach (var key in context.ModelState)
        {
            var errors = key.Value.Errors;
            foreach (var t in errors)
            {
                retErrors.Add(t.ErrorMessage);
            }
        }

        var response = (BadResponse)Value;
        response.Errors = retErrors;
    }
    
}