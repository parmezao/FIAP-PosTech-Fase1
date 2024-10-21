using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Contatos.Web.Infrastructure.CrossCutting.Attributes
{
    /// <summary>
    /// Aceita somente caracteres numéricos e o tamanho deve ser de 11 caracteres.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public sealed partial class TelefoneValidoAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            return value is string valueAsString &&
                (valueAsString.ToString().Length <= 9 &&
                MyRegex().IsMatch(value.ToString() ?? string.Empty));
        }

        [GeneratedRegex("^[0-9]*$")]
        private static partial Regex MyRegex();
    }
}
