using Refit;

namespace VollMed.Consultas.Services
{
    public record MedicoApiResponse(long Id, string Nome);
    public interface IMedicosApi
    {
        [Get("/api/medicos/{id}")]
        Task<MedicoApiResponse> GetByIdAsync(long id);
    }
}
