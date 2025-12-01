using Medicos.ServiceAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medicos.ServiceAPI.Data
{
    public class MedicosDbContext(DbContextOptions<MedicosDbContext> options) : DbContext(options)
    {
        public DbSet<Medico> Medicos { get; set; }
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
