using VollMed.Medicos.Domain.Interfaces;

namespace VollMed.Medicos.Endpoints
{
    public static class DeleteMedico
    {
        public static async Task<IResult> Handle(
            long id,
            IMedicoRepository repository)
        {
            await repository.DeleteByIdAsync(id);
            return Results.NoContent();
        }
    }
}
