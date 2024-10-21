using Contatos.Web.Domain.Entities;

namespace Contatos.Web.Domain.Interfaces;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> UpdateAsync(TEntity entity);
}
