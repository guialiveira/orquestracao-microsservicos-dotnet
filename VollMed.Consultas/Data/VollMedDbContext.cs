using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Domain.Entities;

namespace VollMed.Consultas.Data
{
    public class VollMedDbContext(DbContextOptions<VollMedDbContext> options) : DbContext(options)
    {
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FK relationship
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique indexes
            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.Crm)
                .IsUnique();

            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Cpf)
                .IsUnique();
        }
    }
}
