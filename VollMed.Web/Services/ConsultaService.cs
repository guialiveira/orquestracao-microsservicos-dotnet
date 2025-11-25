using VollMed.Web.Dtos;
using VollMed.Web.Exceptions;
using VollMed.Web.Interfaces;
using VollMed.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace VollMed.Web.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IVollMedApiService _vollMedApiService;

        private const int PageSize = 5;

        public ConsultaService(IConsultaRepository consultaRepository, IVollMedApiService vollMedApiService)
        {
            _consultaRepository = consultaRepository;
            _vollMedApiService = vollMedApiService;
        }
        public async Task<PaginatedList<ConsultaDto>> ListarAsync(int? page)
        {
            var consultas = await _consultaRepository
                .GetAllOrderedByData()
                .ToListAsync();

            var medicosPaginados = await _vollMedApiService.ListarMedicos(null);
            var medicos = medicosPaginados.Items;

            var medicosDict = medicos.ToDictionary(m => m.Id, m => m);

            var dtos = consultas.Select(c =>
            {
                medicosDict.TryGetValue(c.MedicoId, out var medico);

                return new ConsultaDto(
                    c.Id,
                    c.MedicoId,
                    medico?.Nome ?? "Médico não encontrado",
                    c.Paciente,
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

        public async Task<PaginatedList<ConsultaDto>> ListarAsyncOld(int? page)
        {
            var consultas = _consultaRepository.GetAllOrderedByData();
            IQueryable<ConsultaDto> dtos = consultas.Select(m => new ConsultaDto(m));
            return await PaginatedList<ConsultaDto>.CreateAsync(dtos, page ?? 1, PageSize);
        }

        public async Task CadastrarAsync(ConsultaDto dados)
        {
            var medicoConsulta = await _vollMedApiService.ObterFormularioMedico(dados.MedicoId);
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

                //consulta.ModificarDados(medicoConsulta, dados);
                await _consultaRepository.UpdateAsync(consulta);
            }
        }

        public async Task<ConsultaDto> CarregarPorIdAsync(long id)
        {
            var consulta = await _consultaRepository.FindByIdAsync(id);
            if (consulta == null) throw new RegraDeNegocioException("Consulta não encontrada.");

            var medicoConsulta = await _vollMedApiService.ObterFormularioMedico(consulta.MedicoId);
            return new ConsultaDto(consulta.Id, consulta.MedicoId, string.Empty, consulta.Paciente, consulta.Data, medicoConsulta!.Especialidade);
        }

        public async Task ExcluirAsync(long id)
        {
            await _consultaRepository.DeleteByIdAsync(id);
        }
    }
}


