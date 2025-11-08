using Microsoft.EntityFrameworkCore;
using Congress_Backend.Models;
namespace Congress_Backend.Data
{
    public class CongressDbContext : DbContext
    {
        public CongressDbContext()
        {

        }

        public CongressDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Participante> Participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional de la entidad Participante si es necesaria
            modelBuilder.Entity<Participante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
