using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Contatos.Web.Domain.ValueObjects
{
    public class Email
    {
        const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public Email(string endereco)
        {
            if (string.IsNullOrEmpty(endereco) || endereco.Length < 5)
                throw new Exception();

            Endereco = endereco.ToLower().Trim();

            if (!Regex.IsMatch(endereco, pattern))
                throw new Exception();
        }

        public string Endereco { get; }
    }
}
