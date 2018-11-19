using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infra.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Senha)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.SenhaConfirmacao)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.DataAlteracao)
                .IsRequired()
                .HasColumnType("datetime");
        }
    }
}
