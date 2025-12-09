using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Data;
using VollMed.Consultas.Domain.Enums;
using VollMed.Shared.Pagination;

namespace VollMed.Consultas.Endpoints
{
    public static class GetConsultas
    {
        public record ConsultaListResponse(
            long Id,
            long MedicoId,
            string MedicoNome,
            string Paciente,
            string PacienteNome,
            DateTime Data,
            Especialidade Especialidade);

        public static async Task<IResult> Handle(
            int page,
            VollMedDbContext dbContext)
        {
            const int pageSize = 5;

            var consultas = await dbContext.Consultas
                .Include(c => c.Medico)
                .OrderBy(c => c.Data)
                .ToListAsync();

            // Get all pacientes for lookup
            var pacientes = await dbContext.Pacientes.ToListAsync();
            var pacientesDict = pacientes.ToDictionary(p => p.Cpf, p => p);

            var allDtos = consultas.Select(c => new ConsultaListResponse(
                c.Id,
                c.MedicoId,
                c.Medico?.Nome ?? "Médico",
                c.Paciente,
                pacientesDict.ContainsKey(c.Paciente) ? pacientesDict[c.Paciente].Nome : "Paciente",
                c.Data,
                c.Medico?.Especialidade ?? Especialidade.ClinicaGeral
            )).ToList();

            var totalCount = allDtos.Count;
            var items = allDtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginatedList = new PaginatedList<ConsultaListResponse>(items, totalCount, page, pageSize);

            return Results.Ok(paginatedList);
        }
    }
}
