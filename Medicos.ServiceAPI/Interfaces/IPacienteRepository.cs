using Medicos.ServiceAPI.Domain;

namespace Medicos.ServiceAPI.Interfaces
{
    public interface IPacienteRepository
    {
        Task<bool> IsJaCadastradoAsync(string email, string cpf, long? id);
        Task InsertAsync(Paciente paciente);
        Task InsertRangeAsync(IEnumerable<Paciente> pacientes, Action<int> onProgress);
        Task UpdateAsync(Paciente paciente);
        Task<Paciente?> FindByIdAsync(long id);
        Task DeleteByIdAsync(long id);
        Task DeleteAllAsync();
        IQueryable<Paciente> GetAll();
    }
}
