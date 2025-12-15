using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Data;

namespace VollMed.Consultas.Endpoints
{
    public static class PutConsulta
    {
        public record UpdateConsultaRequest([Required] DateTime Data);

        public record UpdateConsultaResponse(
            long Id,
            long MedicoId,
            string Paciente,
            DateTime Data);

        public static async Task<IResult> Handle(
            long id,
            UpdateConsultaRequest request,
            VollMedDbContext dbContext)
        {
            try
            {
                var consulta = await dbContext.Consultas.FindAsync(id);
                if (consulta == null)
                {
                    return Results.NotFound();
                }

                consulta.Data = request.Data;
                await dbContext.SaveChangesAsync();

                var response = new UpdateConsultaResponse(
                    consulta.Id,
                    consulta.MedicoId,
                    consulta.PacienteNome,
                    consulta.Data);

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
