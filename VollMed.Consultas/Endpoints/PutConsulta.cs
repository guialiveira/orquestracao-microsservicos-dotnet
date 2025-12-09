using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Data;

namespace VollMed.Consultas.Endpoints
{
    public static class PutConsulta
    {
        public record UpdateConsultaRequest(
            [Required] long MedicoId,
            [Required, StringLength(11, MinimumLength = 11)] string Paciente,
            [Required] DateTime Data);

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

                var medicoExists = await dbContext.Medicos.AnyAsync(m => m.Id == request.MedicoId);
                if (!medicoExists)
                {
                    return Results.Problem(
                        detail: "Médico não encontrado.",
                        statusCode: StatusCodes.Status400BadRequest);
                }

                consulta.ModificarDados(request.MedicoId, request.Paciente, request.Data);
                await dbContext.SaveChangesAsync();

                var response = new UpdateConsultaResponse(
                    consulta.Id,
                    consulta.MedicoId,
                    consulta.Paciente,
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
