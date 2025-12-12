using Microsoft.EntityFrameworkCore;

namespace VollMed.Consultas.Data
{
    public static class DbSeeder
    {
        public static void Seed(VollMedDbContext context)
        {
            // Aplicar migrações pendentes
            context.Database.Migrate();
        }
    }
}
