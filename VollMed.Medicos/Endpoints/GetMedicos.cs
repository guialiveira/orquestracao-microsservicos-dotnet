using VollMed.Medicos.Domain.Enums;
using VollMed.Medicos.Domain.Interfaces;
using VollMed.Shared.Pagination;

namespace VollMed.Medicos.Endpoints
{
    public static class GetMedicos
    {
        public record MedicoListResponse(
            long Id,
            string Nome,
            string Email,
            string Crm,
            string Telefone,
            Especialidade Especialidade);

        public static IResult Handle(
            int page,
            IMedicoRepository repository)
        {
            const int pageSize = 5;

            var medicos = repository.GetAll();
            var totalCount = medicos.Count();

            var items = medicos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MedicoListResponse(
                    m.Id,
                    m.Nome,
                    m.Email,
                    m.Crm,
                    m.Telefone,
                    m.Especialidade))
                .ToList();

            var paginatedList = new PaginatedList<MedicoListResponse>(items, totalCount, page, pageSize);

            return Results.Ok(paginatedList);
        }
    }
}
