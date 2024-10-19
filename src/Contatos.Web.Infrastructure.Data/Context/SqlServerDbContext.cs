using Contatos.Web.Domain.Entities;
using Contatos.Web.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Web.Infrastructure.Data.Context
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contato>(new ContatoMap().Configure);
        }
    }
}
