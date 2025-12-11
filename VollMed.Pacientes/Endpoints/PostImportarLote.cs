using Bogus;
using Bogus.Extensions.Brazil;
using VollMed.Pacientes.Domain.Entities;
using VollMed.Pacientes.Domain.Interfaces;

namespace VollMed.Pacientes.Endpoints
{
    public static class PostImportarLote
    {
        public record Response(
            int TotalImportados,
            string TempoDecorrido,
            string Mensagem);

        public static async Task<IResult> Handle(
            int quantidade,
            IPacienteRepository repository,
            ILogger logger)
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

            var pacientes = faker.Generate(quantidade);

            // Import with progress logging
            await repository.InsertRangeAsync(pacientes, count =>
            {
                logger.LogInformation($"Importados {count} de {quantidade} pacientes...");
            });

            stopwatch.Stop();

            var response = new Response(
                quantidade,
                $"{stopwatch.ElapsedMilliseconds}ms",
                $"{quantidade} pacientes importados com sucesso (com lock de tabela demonstrado)");

            return Results.Ok(response);
        }
    }
}
