using VollMed.Pacientes.Domain.Interfaces;
using VollMed.Shared.Pagination;

namespace VollMed.Pacientes.Endpoints
{
    public static class GetPacientes
    {
        public record PacienteListResponse(
            long Id,
            string Nome,
            string Cpf,
            string Email,
            string Telefone);

        public static IResult Handle(
            int page,
            IPacienteRepository repository)
        {
            const int pageSize = 5;

            var pacientes = repository.GetAll();
            var totalCount = pacientes.Count();

            var items = pacientes
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PacienteListResponse(
                    p.Id,
                    p.Nome,
                    p.Cpf,
                    p.Email,
                    p.Telefone))
                .ToList();

            var paginatedList = new PaginatedList<PacienteListResponse>(items, totalCount, page, pageSize);

            return Results.Ok(paginatedList);
        }
    }
}
