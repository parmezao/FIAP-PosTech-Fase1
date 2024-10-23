using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Contatos.Web.Domain.Attributes;

/// <summary>
/// Aceita somente caracteres numéricos e o tamanho deve ser de 2 caracteres.
/// </summary>
public sealed partial class DDDValidoAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        return value is int valueAsInt &&
            (valueAsInt.ToString().Length == 2 &&
            DDDRegex().IsMatch(value.ToString() ?? string.Empty));
    }

    [GeneratedRegex("^[1-9]*$")]
    private static partial Regex DDDRegex();
}

