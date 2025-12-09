using System.ComponentModel.DataAnnotations;
using VollMed.Pacientes.Domain.Entities;
using VollMed.Pacientes.Domain.Interfaces;

namespace VollMed.Pacientes.Endpoints
{
    public static class PostPaciente
    {
        public record CreatePacienteRequest(
            [Required, MinLength(1)] string Nome,
            [Required, StringLength(11, MinimumLength = 11)] string Cpf,
            [Required, EmailAddress] string Email,
            [Required] string Telefone);

        public record CreatePacienteResponse(
            long Id,
            string Nome,
            string Cpf,
            string Email,
            string Telefone);

        public static async Task<IResult> Handle(
            CreatePacienteRequest request,
            IPacienteRepository repository)
        {
            try
            {
                if (await repository.IsJaCadastradoAsync(request.Email, request.Cpf, null))
                {
                    return Results.Problem(
                        detail: "E-mail ou CPF já cadastrado para outro paciente!",
                        statusCode: StatusCodes.Status400BadRequest);
                }

                var paciente = new Paciente(
                    request.Nome,
                    request.Cpf,
                    request.Email,
                    request.Telefone);

                await repository.InsertAsync(paciente);

                var response = new CreatePacienteResponse(
                    paciente.Id,
                    paciente.Nome,
                    paciente.Cpf,
                    paciente.Email,
                    paciente.Telefone);

                return Results.Created($"/api/pacientes/{paciente.Id}", response);
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
