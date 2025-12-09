using VollMed.Pacientes.Domain.Interfaces;

namespace VollMed.Pacientes.Endpoints
{
    public static class GetPacienteById
    {
        public record PacienteDetailResponse(
            long Id,
            string Nome,
            string Cpf,
            string Email,
            string Telefone);

        public static async Task<IResult> Handle(
            long id,
            IPacienteRepository repository)
        {
            if (id == 0)
            {
                return Results.Ok(new PacienteDetailResponse(0, "", "", "", ""));
            }

            var paciente = await repository.FindByIdAsync(id);
            if (paciente == null)
            {
                return Results.NotFound();
            }

            var response = new PacienteDetailResponse(
                paciente.Id,
                paciente.Nome,
                paciente.Cpf,
                paciente.Email,
                paciente.Telefone);

            return Results.Ok(response);
        }
    }
}
