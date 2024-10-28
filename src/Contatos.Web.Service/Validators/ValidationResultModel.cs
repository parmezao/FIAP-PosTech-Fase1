using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contatos.Web.Service.Validators;

public class ValidationResultModel
{
    public string Message { get; }

    public List<ValidationError> Errors { get; }

    public ValidationResultModel(ModelStateDictionary modelState)
    {
        Message = "Falha na Validação";
        Errors = modelState.Keys
            .SelectMany(key => modelState[key]!.Errors
                .Select(x => new ValidationError(key, x.ErrorMessage))).ToList();
    }
}
