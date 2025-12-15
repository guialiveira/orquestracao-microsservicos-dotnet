using Refit;

namespace VollMed.Consultas.Services
{
    public record PacienteApiResponse(long Id, string Nome);
    public interface IPacientesApi
    {
        [Get("/api/pacientes/{id}")]
        Task<PacienteApiResponse> GetByIdAsync(long id);
    }
}
