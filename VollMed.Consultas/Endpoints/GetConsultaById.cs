using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Data;

namespace VollMed.Consultas.Endpoints
{
    public static class GetConsultaById
    {
        public record ConsultaDetailResponse(
            long Id,
            long MedicoId,
            string MedicoNome,
            long PacienteId,
            string? PacienteNome,
            DateTime Data);

        public static async Task<IResult> Handle(
            long id,
            VollMedDbContext dbContext)
        {
            if (id == 0)
            {
                return Results.NotFound();
            }

            var consulta = await dbContext.Consultas
                .SingleOrDefaultAsync(c => c.Id == id);

            if (consulta == null)
            {
                return Results.NotFound();
            }

            var response = new ConsultaDetailResponse(
                consulta.Id,
                consulta.MedicoId,
                consulta.MedicoNome ?? "",
                consulta.PacienteId,
                consulta.PacienteNome,
                consulta.Data);

            return Results.Ok(response);
        }
    }
}
