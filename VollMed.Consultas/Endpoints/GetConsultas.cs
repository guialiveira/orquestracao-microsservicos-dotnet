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
            long PacienteId,
            string PacienteNome,
            DateTime Data);

        public static async Task<IResult> Handle(
            int page,
            VollMedDbContext dbContext)
        {
            const int pageSize = 5;

            var consultas = await dbContext.Consultas
                .OrderBy(c => c.Data)
                .ToListAsync();

            var allDtos = consultas.Select(c => new ConsultaListResponse(
                c.Id,
                c.MedicoId,
                c.MedicoNome,
                c.PacienteId,
                c.PacienteNome,
                c.Data
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
