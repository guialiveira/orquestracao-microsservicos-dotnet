using Microsoft.EntityFrameworkCore;
using VollMed.Medicos.Domain.Entities;

namespace VollMed.Medicos.Data
{
    public class VollMedDbContext(DbContextOptions<VollMedDbContext> options) : DbContext(options)
    {
        // This service only interacts with Medicos
        public DbSet<Medico> Medicos { get; set; }

        // Included for shared database context (same database as other services)
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relacionamento entre Consulta e Medico
            modelBuilder.Entity<Consulta>()
                .HasOne<Medico>()
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar índices únicos para Medico
            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.Crm)
                .IsUnique();

            // Configurar índices únicos para Paciente
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Cpf)
                .IsUnique();
        }
    }
}
