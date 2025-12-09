using System.ComponentModel.DataAnnotations;
using VollMed.Pacientes.Domain.Interfaces;

namespace VollMed.Pacientes.Endpoints
{
    public static class PutPaciente
    {
        public record UpdatePacienteRequest(
            [Required, MinLength(1)] string Nome,
            [Required, StringLength(11, MinimumLength = 11)] string Cpf,
            [Required, EmailAddress] string Email,
            [Required] string Telefone);

        public record UpdatePacienteResponse(
            long Id,
            string Nome,
            string Cpf,
            string Email,
            string Telefone);

        public static async Task<IResult> Handle(
            long id,
            UpdatePacienteRequest request,
            IPacienteRepository repository)
        {
            try
            {
                var paciente = await repository.FindByIdAsync(id);
                if (paciente == null)
                {
                    return Results.NotFound();
                }

                if (await repository.IsJaCadastradoAsync(request.Email, request.Cpf, id))
                {
                    return Results.Problem(
                        detail: "E-mail ou CPF já cadastrado para outro paciente!",
                        statusCode: StatusCodes.Status400BadRequest);
                }

                paciente.AtualizarDados(
                    request.Nome,
                    request.Cpf,
                    request.Email,
                    request.Telefone);

                await repository.UpdateAsync(paciente);

                var response = new UpdatePacienteResponse(
                    paciente.Id,
                    paciente.Nome,
                    paciente.Cpf,
                    paciente.Email,
                    paciente.Telefone);

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
