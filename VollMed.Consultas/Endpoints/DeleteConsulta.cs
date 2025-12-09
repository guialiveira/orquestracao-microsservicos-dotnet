using VollMed.Consultas.Data;

namespace VollMed.Consultas.Endpoints
{
    public static class DeleteConsulta
    {
        public static async Task<IResult> Handle(
            long id,
            VollMedDbContext dbContext)
        {
            var consulta = await dbContext.Consultas.FindAsync(id);
            if (consulta != null)
            {
                dbContext.Consultas.Remove(consulta);
                await dbContext.SaveChangesAsync();
            }
            return Results.NoContent();
        }
    }
}
