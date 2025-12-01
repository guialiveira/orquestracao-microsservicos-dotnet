using Medicos.ServiceAPI.Dto;

namespace Medicos.ServiceAPI.Interfaces
{
    public interface IConsultaService
    {
        Task CadastrarAsync(ConsultaDto dados);
        Task<ConsultaDto> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<PaginatedList<ConsultaDto>> ListarAsync(int? page);
    }
}
