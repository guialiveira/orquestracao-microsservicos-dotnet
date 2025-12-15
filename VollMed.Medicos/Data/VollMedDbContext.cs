using Microsoft.EntityFrameworkCore;
using VollMed.Medicos.Domain.Entities;

namespace VollMed.Medicos.Data
{
    public class VollMedDbContext(DbContextOptions<VollMedDbContext> options) : DbContext(options)
    {
        // This service only interacts with Medicos
        public DbSet<Medico> Medicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar índices únicos para Medico
            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.Crm)
                .IsUnique();
        }
    }
}
