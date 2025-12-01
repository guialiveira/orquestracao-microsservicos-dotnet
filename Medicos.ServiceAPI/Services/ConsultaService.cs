using Medicos.ServiceAPI.Dto;
using Medicos.ServiceAPI.Exceptions;
using Medicos.ServiceAPI.Interfaces;
using Medicos.ServiceAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medicos.ServiceAPI.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;

        private const int PageSize = 5;

        public ConsultaService(IConsultaRepository consultaRepository, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository)
        {
            _consultaRepository = consultaRepository;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<PaginatedList<ConsultaDto>> ListarAsync(int? page)
        {
            var consultas = await _consultaRepository
                .GetAllOrderedByData()
                .ToListAsync();

            var medicos = await _medicoRepository
                .GetAll()
                .ToListAsync();

            var pacientes = await _pacienteRepository
                .GetAll()
                .ToListAsync();

            var medicosDict = medicos.ToDictionary(m => m.Id, m => m);
            var pacientesDict = pacientes.ToDictionary(p => p.Cpf, p => p);

            var dtos = consultas.Select(c =>
            {
                medicosDict.TryGetValue(c.MedicoId, out var medico);
                pacientesDict.TryGetValue(c.Paciente, out var paciente);

                return new ConsultaDto(
                    c.Id,
                    c.MedicoId,
                    medico?.Nome ?? "Médico não encontrado",
                    c.Paciente,
                    paciente?.Nome ?? "Paciente não encontrado",
                    c.Data,
                    medico?.Especialidade ?? 0
                );
            }).ToList();

            // paginação manual
            var pageNumber = page ?? 1;
            var totalItems = dtos.Count();
            var items = dtos
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return new PaginatedList<ConsultaDto>(
                items,
                totalItems,
                pageNumber,
                PageSize
            );
        }

        public async Task CadastrarAsync(ConsultaDto dados)
        {
            var medicoConsulta = await _medicoRepository.FindByIdAsync(dados.MedicoId);
            if (medicoConsulta == null)
            {
                throw new RegraDeNegocioException("Medico não encontrado.");
            }
            else
            {
                dados.MedicoId = medicoConsulta.Id;
                dados.MedicoNome = medicoConsulta.Nome;
            }

            if (dados.Id == 0)
            {
                var consulta = new Consulta(dados);
                await _consultaRepository.SaveAsync(consulta);
            }
            else
            {
                var consulta = await _consultaRepository.FindByIdAsync(dados.Id);
                if (consulta == null) throw new RegraDeNegocioException("Consulta não encontrada.");

                consulta.ModificarDados(dados);
                await _consultaRepository.UpdateAsync(consulta);
            }
        }

        public async Task<ConsultaDto> CarregarPorIdAsync(long id)
        {
            var consulta = await _consultaRepository.FindByIdAsync(id);
            if (consulta == null) throw new RegraDeNegocioException("Consulta não encontrada.");

            var medicoConsulta = await _medicoRepository.FindByIdAsync(consulta.MedicoId);
            var pacienteConsulta = await _pacienteRepository.GetAll()
                .Where(p => p.Cpf == consulta.Paciente)
                .FirstOrDefaultAsync();

            return new ConsultaDto(
                consulta.Id,
                consulta.MedicoId,
                medicoConsulta?.Nome ?? string.Empty,
                consulta.Paciente,
                pacienteConsulta?.Nome,
                consulta.Data,
                medicoConsulta!.Especialidade
            );
        }

        public async Task ExcluirAsync(long id)
        {
            await _consultaRepository.DeleteByIdAsync(id);
        }
    }
}
