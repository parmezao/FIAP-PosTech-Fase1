using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.Interfaces;
using FluentValidation;

namespace Contatos.Web.Service.Services;

public class BaseService<TEntity>(IBaseRepository<TEntity> baseRepository) 
    : IBaseService<TEntity> where TEntity : BaseEntity
{
    protected readonly IBaseRepository<TEntity> _baseRepository = baseRepository;

    public async Task<TEntity> AddAsync<TValidator>(TEntity entity) where TValidator 
        : AbstractValidator<TEntity>
    {
        await Validate(entity, Activator.CreateInstance<TValidator>());
        await _baseRepository.InsertAsync(entity);

        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        await _baseRepository.DeleteAsync(id);
    }

    public async Task<IList<TEntity>> GetAllAsync() => await _baseRepository.SelectAllAsync();    

    public async Task<TEntity> GetByIdAsync(int id) => await _baseRepository.SelectAsync(id);    

    public async Task<TEntity> UpdateAsync<TValidator>(TEntity entity) where TValidator 
        : AbstractValidator<TEntity>
    {
        await Validate(entity, Activator.CreateInstance<TValidator>());
        await _baseRepository.UpdateAsync(entity);

        return entity;
    }

    private static async Task Validate(TEntity entity, AbstractValidator<TEntity> validator)
    {
        if (entity == null)
            throw new Exception("Registros não localizados!");

        await validator.ValidateAndThrowAsync(entity);
    }    
}
