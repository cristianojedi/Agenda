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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contato>(new ContatoMap().Configure);
        }
    }
}
