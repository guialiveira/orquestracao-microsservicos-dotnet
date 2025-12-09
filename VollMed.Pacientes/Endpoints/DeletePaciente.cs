using VollMed.Pacientes.Domain.Interfaces;

namespace VollMed.Pacientes.Endpoints
{
    public static class DeletePaciente
    {
        public static async Task<IResult> Handle(
            long id,
            IPacienteRepository repository)
        {
            await repository.DeleteByIdAsync(id);
            return Results.NoContent();
        }
    }
}
