using VollMed.Medicos.Domain.Enums;
using VollMed.Medicos.Domain.Interfaces;

namespace VollMed.Medicos.Endpoints
{
    public static class GetMedicoById
    {
        public record MedicoDetailResponse(
            long Id,
            string Nome,
            string Email,
            string Crm,
            string Telefone,
            Especialidade Especialidade);

        public static async Task<IResult> Handle(
            long id,
            IMedicoRepository repository)
        {
            if (id == 0)
            {
                // Return empty response for new medico form
                return Results.Ok(new MedicoDetailResponse(0, "", "", "", "", Especialidade.ClinicaGeral));
            }

            var medico = await repository.FindByIdAsync(id);
            if (medico == null)
            {
                return Results.NotFound();
            }

            var response = new MedicoDetailResponse(
                medico.Id,
                medico.Nome,
                medico.Email,
                medico.Crm,
                medico.Telefone,
                medico.Especialidade);

            return Results.Ok(response);
        }
    }
}
