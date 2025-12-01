using VollMed.Web.Dtos;
using VollMed.Web.Exceptions;
using VollMed.Web.Interfaces;
using VollMed.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace VollMed.Web.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IVollMedApiService _vollMedApiService;

        public ConsultaService(IVollMedApiService vollMedApiService)
        {
            _vollMedApiService = vollMedApiService;
        }

        public async Task<PaginatedList<ConsultaDto>> ListarAsync(int? page)
        {
            return await _vollMedApiService.ListarConsultas(page);
        }

        public async Task CadastrarAsync(ConsultaDto dados)
        {
            await _vollMedApiService.SalvarConsulta(dados);
        }

        public async Task<ConsultaDto> CarregarPorIdAsync(long id)
        {
            return await _vollMedApiService.ObterFormularioConsulta(id);
        }

        public async Task ExcluirAsync(long id)
        {
            await _vollMedApiService.ExcluirConsulta(id);
        }
    }
}


