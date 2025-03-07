using System.Text.Json.Serialization;

namespace Contatos.Web.Service.Validators;

public class ValidationError
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Field { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Code { get; set; }

    public string Message { get; }

    public ValidationError(string field, string message)
    {
        Field = field != string.Empty ? field : null;
        Message = message;
    }
}
