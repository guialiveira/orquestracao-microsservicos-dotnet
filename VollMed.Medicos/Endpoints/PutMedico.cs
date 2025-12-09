using System.ComponentModel.DataAnnotations;
using VollMed.Medicos.Domain.Enums;
using VollMed.Medicos.Domain.Interfaces;

namespace VollMed.Medicos.Endpoints
{
    public static class PutMedico
    {
        public record UpdateMedicoRequest(
            [Required, MinLength(1)] string Nome,
            [Required, EmailAddress] string Email,
            [Required, StringLength(6, MinimumLength = 4)] string Crm,
            [Required] string Telefone,
            [Required] Especialidade Especialidade);

        public record UpdateMedicoResponse(
            long Id,
            string Nome,
            string Email,
            string Crm,
            string Telefone,
            Especialidade Especialidade);

        public static async Task<IResult> Handle(
            long id,
            UpdateMedicoRequest request,
            IMedicoRepository repository)
        {
            try
            {
                var medico = await repository.FindByIdAsync(id);
                if (medico == null)
                {
                    return Results.NotFound();
                }

                // Validate email/crm not already registered for another medico
                if (await repository.IsJaCadastradoAsync(request.Email, request.Crm, id))
                {
                    return Results.Problem(
                        detail: "E-mail ou CRM já cadastrado para outro médico!",
                        statusCode: StatusCodes.Status400BadRequest);
                }

                medico.AtualizarDados(
                    request.Nome,
                    request.Email,
                    request.Telefone,
                    request.Crm,
                    request.Especialidade);

                await repository.UpdateAsync(medico);

                var response = new UpdateMedicoResponse(
                    medico.Id,
                    medico.Nome,
                    medico.Email,
                    medico.Crm,
                    medico.Telefone,
                    medico.Especialidade);

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
