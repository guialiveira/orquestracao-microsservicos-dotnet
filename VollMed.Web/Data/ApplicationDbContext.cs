using VollMed.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace VollMed.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Consulta> Consultas { get; set; }
        
        //public DbSet<Medico> Medicos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Consulta>().Property(c => c.MedicoId).IsRequired();
                
        }

    }
}


