using Medicos.ServiceAPI.Domain;
using Medicos.ServiceAPI.Dto;
using Medicos.ServiceAPI.Exceptions;
using Medicos.ServiceAPI.Interfaces;
using System.Diagnostics;

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

            Console.WriteLine($"\n=== Iniciando importação de {quantidade} pacientes ===");

            await _repository.DeleteAllAsync();

            // Gerar todos os pacientes
            var pacientes = new List<Paciente>();
            for (int i = 1; i <= quantidade; i++)
            {
                var dto = new PacienteDto
                {
                    Nome = $"Paciente Teste {i}",
                    Cpf = $"{10000000000L + i}",
                    Email = $"paciente{i}@teste.com",
                    Telefone = $"(11) 9{i:D8}"
                };
                pacientes.Add(new Paciente(dto));
            }

            // Inserir com transação (lock mantido durante toda operação)
            await _repository.InsertRangeAsync(pacientes, count =>
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Importados {count} pacientes...");
            });

            sw.Stop();
            Console.WriteLine($"=== Importação concluída em {sw.Elapsed.TotalSeconds:F2}s ===\n");

            return new
            {
                QuantidadeImportada = quantidade,
                TempoDecorrido = $"{sw.Elapsed.TotalSeconds:F2}s",
                Mensagem = "Importação concluída com sucesso"
            };
        }
    }
}
