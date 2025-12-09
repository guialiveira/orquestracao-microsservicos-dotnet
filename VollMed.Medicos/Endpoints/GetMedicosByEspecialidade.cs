using VollMed.Medicos.Domain.Enums;
using VollMed.Medicos.Domain.Interfaces;

namespace VollMed.Medicos.Endpoints
{
    public static class GetMedicosByEspecialidade
    {
        public record MedicosByEspecialidadeResponse(
            long Id,
            string Nome,
            string Email,
            string Crm,
            string Telefone,
            Especialidade Especialidade);

        public static async Task<IResult> Handle(
            string especialidade,
            IMedicoRepository repository)
        {
            if (!Enum.TryParse<Especialidade>(especialidade, out var especEnum))
            {
                return Results.BadRequest("Especialidade inválida.");
            }

            var medicos = await repository.FindByEspecialidadeAsync(especEnum);

            var responses = medicos.Select(m => new MedicosByEspecialidadeResponse(
                m.Id,
                m.Nome,
                m.Email,
                m.Crm,
                m.Telefone,
                m.Especialidade)).ToList();

            return Results.Ok(responses);
        }
    }
}
