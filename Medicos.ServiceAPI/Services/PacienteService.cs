using Medicos.ServiceAPI.Domain;
using Medicos.ServiceAPI.Dto;
using Medicos.ServiceAPI.Exceptions;
using Medicos.ServiceAPI.Interfaces;
using System.Diagnostics;
using System.Transactions;

namespace Medicos.ServiceAPI.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repository;
        private const int PageSize = 5;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<PacienteDto>> ListarAsync(int? page)
        {
            var pacientes = _repository.GetAll();
            IQueryable<PacienteDto> dtos = pacientes.Select(p => new PacienteDto(p));
            return await PaginatedList<PacienteDto>.CreateAsync(dtos, page ?? 1, PageSize);
        }

        public IEnumerable<PacienteDto> ListarTodos()
        {
            var pacientes = _repository.GetAll();
            return pacientes.Select(p => new PacienteDto(p)).AsEnumerable();
        }

        public async Task CadastrarAsync(PacienteDto dados)
        {
            if (await _repository.IsJaCadastradoAsync(dados.Email, dados.Cpf, dados.Id))
            {
                throw new RegraDeNegocioException("E-mail ou CPF já cadastrado para outro paciente!");
            }

            if (dados.Id == null)
            {
                var paciente = new Paciente(dados);
                await _repository.InsertAsync(paciente);
            }
            else
            {
                var paciente = await _repository.FindByIdAsync(dados.Id.Value);
                if (paciente == null) throw new RegraDeNegocioException("Paciente não encontrado.");

                paciente.AtualizarDados(dados);
                await _repository.UpdateAsync(paciente);
            }
        }

        public async Task<PacienteDto> CarregarPorIdAsync(long id)
        {
            var paciente = await _repository.FindByIdAsync(id);
            if (paciente == null) throw new RegraDeNegocioException("Paciente não encontrado.");

            return new PacienteDto(paciente);
        }

        public async Task ExcluirAsync(long id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<object> ImportarLoteAsync(int quantidade)
        {
            var sw = Stopwatch.StartNew();
            int batchSize = 50;
            int totalBatches = quantidade / batchSize;

            // TransactionScope mantém o lock ativo durante toda a operação
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Console.WriteLine($"\n=== Iniciando importação de {quantidade} pacientes ===");

                for (int batch = 0; batch < totalBatches; batch++)
                {
                    for (int i = 0; i < batchSize; i++)
                    {
                        int numero = (batch * batchSize) + i + 1;
                        var pacienteDto = new PacienteDto
                        {
                            Nome = $"Paciente Teste {numero}",
                            Cpf = $"{10000000000L + numero}",
                            Email = $"paciente{numero}@teste.com",
                            Telefone = $"(11) 9{numero:D8}"
                        };

                        var paciente = new Paciente(pacienteDto);
                        await _repository.InsertAsync(paciente);
                    }

                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Importando lote {batch + 1}/{totalBatches}... ({(batch + 1) * batchSize} pacientes inseridos)");

                    // Lock MANTIDO aqui porque TransactionScope ainda está aberto
                    Thread.Sleep(1000);
                }

                // Comitar transação - lock liberado só aqui
                scope.Complete();

                sw.Stop();
                Console.WriteLine($"=== Importação concluída em {sw.Elapsed.TotalSeconds:F2}s ===\n");
            }

            return new
            {
                QuantidadeImportada = quantidade,
                TempoDecorrido = $"{sw.Elapsed.TotalSeconds:F2}s",
                Mensagem = "Importação concluída com sucesso"
            };
        }
    }
}
