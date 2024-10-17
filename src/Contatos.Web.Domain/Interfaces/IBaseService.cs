using Contatos.Web.Domain.Entities;
using FluentValidation;

namespace Contatos.Web.Domain.Interfaces;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>;
    Task DeleteAsync(int id);
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> UpdateAsync<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>;
}
