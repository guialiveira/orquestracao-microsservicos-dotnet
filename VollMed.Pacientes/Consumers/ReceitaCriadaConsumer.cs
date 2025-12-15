using MassTransit;
using VollMed.Pacientes.Domain.Interfaces;
using VollMed.Shared.Eventos;

namespace VollMed.Pacientes.Consumers
{
    public class ReceitaCriadaConsumer : IConsumer<ReceitaCriadaEvent>
    {
        private readonly IPacienteRepository repository;
        private readonly ILogger<ReceitaCriadaConsumer> logger;

        public ReceitaCriadaConsumer(IPacienteRepository repository, ILogger<ReceitaCriadaConsumer> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ReceitaCriadaEvent> context)
        {
            var paciente = await repository
                .FindByIdAsync(context.Message.PacienteId);
            if (paciente is null) return;

            logger.LogInformation("Enviando receita para paciente {PacienteNome}", paciente.Nome);
            logger.LogInformation("Email do paciente: {PacienteEmail}", paciente.Email);
            logger.LogInformation("Receita: {Receita}", context.Message.Receita);
        }
    }
}
