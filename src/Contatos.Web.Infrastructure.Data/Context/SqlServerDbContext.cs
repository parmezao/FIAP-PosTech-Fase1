using Contatos.Web.Domain.Entities;
using Contatos.Web.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Web.Infrastructure.Data.Context
{
    public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
    {
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contato>(new ContatoMap().Configure);
        }
    }
}
