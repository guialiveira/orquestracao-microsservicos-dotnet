using System.ComponentModel.DataAnnotations;
using VollMed.Medicos.Domain.Entities;
using VollMed.Medicos.Domain.Enums;
using VollMed.Medicos.Domain.Interfaces;

namespace VollMed.Medicos.Endpoints
{
    public static class PostMedico
    {
        public record CreateMedicoRequest(
            [Required, MinLength(1)] string Nome,
            [Required, EmailAddress] string Email,
            [Required, StringLength(6, MinimumLength = 4)] string Crm,
            [Required] string Telefone,
            [Required] Especialidade Especialidade);

        public record CreateMedicoResponse(
            long Id,
            string Nome,
            string Email,
            string Crm,
            string Telefone,
            Especialidade Especialidade);

        public static async Task<IResult> Handle(
            CreateMedicoRequest request,
            IMedicoRepository repository)
        {
            try
            {
                // Validate email/crm not already registered
                if (await repository.IsJaCadastradoAsync(request.Email, request.Crm, null))
                {
                    return Results.Problem(
                        detail: "E-mail ou CRM já cadastrado para outro médico!",
                        statusCode: StatusCodes.Status400BadRequest);
                }

                var medico = new Medico(
                    request.Nome,
                    request.Email,
                    request.Telefone,
                    request.Crm,
                    request.Especialidade);

                await repository.InsertAsync(medico);

                var response = new CreateMedicoResponse(
                    medico.Id,
                    medico.Nome,
                    medico.Email,
                    medico.Crm,
                    medico.Telefone,
                    medico.Especialidade);

                return Results.Created($"/api/medicos/{medico.Id}", response);
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
