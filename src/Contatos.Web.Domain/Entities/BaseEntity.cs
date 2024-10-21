using System.Text.Json.Serialization;

namespace Contatos.Web.Domain.Entities;

public abstract class BaseEntity
{
    public virtual int Id { get; set; }
}
