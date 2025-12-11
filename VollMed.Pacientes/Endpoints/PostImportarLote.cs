using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Mvc;
using VollMed.Pacientes.Domain.Entities;
using VollMed.Pacientes.Domain.Interfaces;

namespace VollMed.Pacientes.Endpoints
{
    public static class PostImportarLote
    {
        public record PostImportarRequest(int Quantidade);
        public record Response(
            int TotalImportados,
            string TempoDecorrido,
            string Mensagem);

        public static async Task<IResult> Handle(
            PostImportarRequest request,
            IPacienteRepository repository,
            ILogger<PostImportarRequest> logger)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            await repository.DeleteAllAsync();

            // Generate fake patients using Bogus
            var faker = new Faker<Paciente>("pt_BR")
                .CustomInstantiator(f => new Paciente(
                    f.Name.FullName(),
                    f.Person.Cpf(false),
                    f.Internet.Email(),
                    f.Phone.PhoneNumber("(##) #####-####")));

            var pacientes = faker.Generate(request.Quantidade).DistinctBy(p => p.Email);

            // Import with progress logging
            await repository.InsertRangeAsync(pacientes, count =>
            {
                logger.LogInformation($"Importados {count} de {request.Quantidade} pacientes...");
            });

            stopwatch.Stop();

            var response = new Response(
                request.Quantidade,
                $"{stopwatch.ElapsedMilliseconds}ms",
                $"{request.Quantidade} pacientes importados com sucesso (com lock de tabela demonstrado)");

            return Results.Ok(response);
        }
    }
}
