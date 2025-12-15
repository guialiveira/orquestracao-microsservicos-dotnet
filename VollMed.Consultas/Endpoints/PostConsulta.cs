using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Data;
using VollMed.Consultas.Domain.Entities;
using VollMed.Consultas.Services;

namespace VollMed.Consultas.Endpoints
{
    public static class PostConsulta
    {
        public record CreateConsultaRequest(
            [Required] long MedicoId,
            [Required] long PacienteId,
            [Required] DateTime Data);

        public record CreateConsultaResponse(
            long Id,
            long MedicoId,
            string MedicoNome,
            long PacienteId,
            string PacienteNome,
            DateTime Data);

        public static async Task<IResult> Handle(
            CreateConsultaRequest request,
            VollMedDbContext dbContext,
            IMedicosApi medicosApi,
            IPacientesApi pacientesApi)
        {
            try
            {
                var medico = await medicosApi.GetByIdAsync(request.MedicoId);
                var paciente = await pacientesApi.GetByIdAsync(request.PacienteId);

                var consulta = new Consulta(
                    medicoId: request.MedicoId
                    , medicoNome: medico?.Nome ?? "Médico Desconhecido"
                    , pacienteId: request.PacienteId
                    , pacienteNome: paciente?.Nome ?? "Paciente Desconhecido"
                    , request.Data);

                await dbContext.Consultas.AddAsync(consulta);
                await dbContext.SaveChangesAsync();

                var response = new CreateConsultaResponse(
                    consulta.Id,
                    consulta.MedicoId,
                    consulta.MedicoNome,
                    consulta.PacienteId,
                    consulta.PacienteNome,
                    consulta.Data);

                return Results.Created($"/api/consultas/{consulta.Id}", response);
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
