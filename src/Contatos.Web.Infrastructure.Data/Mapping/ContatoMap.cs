using Contatos.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contatos.Web.Infrastructure.Data.Mapping;

public class ContatoMap : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contato");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            //.HasConversion(x => x.ToString(), x => x)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Email)
                        //.HasConversion(x => x.ToString(), x => x)
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("varchar(80)");

        builder.Property(x => x.Telefone)
                        //.HasConversion(x => x.ToString(), x => x)
                        .IsRequired()
                        .HasColumnName("Telefone")
                        .HasColumnType("varchar(50)");
    }
}

