using Medicos.ServiceAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medicos.ServiceAPI.Data
{
    public class MedicosDbContext(DbContextOptions<MedicosDbContext> options) : DbContext(options)
    {
        public DbSet<Medico> Medicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
