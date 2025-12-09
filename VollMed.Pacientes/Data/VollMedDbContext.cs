using Microsoft.EntityFrameworkCore;
using VollMed.Pacientes.Domain.Entities;

namespace VollMed.Pacientes.Data
{
    public class VollMedDbContext(DbContextOptions<VollMedDbContext> options) : DbContext(options)
    {
        // This service only interacts with Pacientes
        public DbSet<Paciente> Pacientes { get; set; }

        // Included for shared database context
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar índices únicos para Paciente
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Cpf)
                .IsUnique();

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
