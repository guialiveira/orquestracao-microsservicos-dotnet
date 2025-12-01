using Medicos.ServiceAPI.Dto;

namespace Medicos.ServiceAPI.Interfaces
{
    public interface IPacienteService
    {
        Task CadastrarAsync(PacienteDto dados);
        Task<PacienteDto> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<PaginatedList<PacienteDto>> ListarAsync(int? page);
        IEnumerable<PacienteDto> ListarTodos();
    }
}
