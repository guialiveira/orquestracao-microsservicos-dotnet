using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Domain.Entities;

namespace VollMed.Consultas.Data
{
    public class VollMedDbContext(DbContextOptions<VollMedDbContext> options) : DbContext(options)
    {
        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
