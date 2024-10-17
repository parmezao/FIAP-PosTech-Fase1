using Contatos.Web.Domain.Entities;

namespace Contatos.Web.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task<IList<TEntity>> SelectAllAsync();
    Task<TEntity> SelectAsync(int id);
}
