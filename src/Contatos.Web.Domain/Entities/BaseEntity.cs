using System.Text.Json.Serialization;

namespace Contatos.Web.Domain.Entities;

public abstract class BaseEntity
{
    [JsonIgnore]
    public virtual int Id { get; set; }
}
