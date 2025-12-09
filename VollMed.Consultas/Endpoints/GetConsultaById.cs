using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Data;
using VollMed.Consultas.Domain.Enums;

namespace VollMed.Consultas.Endpoints
{
    public static class GetConsultaById
    {
        public record ConsultaDetailResponse(
            long Id,
            long MedicoId,
            string MedicoNome,
            string Paciente,
            string? PacienteNome,
            DateTime Data,
            Especialidade Especialidade);

        public static async Task<IResult> Handle(
            long id,
            VollMedDbContext dbContext)
        {
            if (id == 0)
            {
                return Results.Ok(new ConsultaDetailResponse(0, 0, "", "", "", DateTime.Now, Especialidade.ClinicaGeral));
            }

            var consulta = await dbContext.Consultas
                .Include(c => c.Medico)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (consulta == null)
            {
                return Results.NotFound();
            }

            var paciente = await dbContext.Pacientes
                .Where(p => p.Cpf == consulta.Paciente)
                .FirstOrDefaultAsync();

            var response = new ConsultaDetailResponse(
                consulta.Id,
                consulta.MedicoId,
                consulta.Medico?.Nome ?? "",
                consulta.Paciente,
                paciente?.Nome,
                consulta.Data,
                consulta.Medico?.Especialidade ?? Especialidade.ClinicaGeral);

            return Results.Ok(response);
        }
    }
}
