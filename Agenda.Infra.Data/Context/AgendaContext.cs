using Agenda.Domain.Entities;
using Agenda.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infra.Data.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext()
        {

        }

        public AgendaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contato> Contato { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contato>(new ContatoMap().Configure);
            modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
        }
    }
}
