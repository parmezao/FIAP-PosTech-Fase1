using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.Interfaces;
using Contatos.Web.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Web.Infrastructure.Data.Repository;

public class BaseRepository<TEntity>(SqlServerDbContext context) 
    : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly SqlServerDbContext _context = context;

    public async Task DeleteAsync(int id)
    {
        _context.Set<TEntity>().Remove(SelectAsync(id).Result);
        await _context.SaveChangesAsync();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<TEntity>> SelectAllAsync() => await _context.Set<TEntity>().ToListAsync();
                  
    public async Task<TEntity> SelectAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
