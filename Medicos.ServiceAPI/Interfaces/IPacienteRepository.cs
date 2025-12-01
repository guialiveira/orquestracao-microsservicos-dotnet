using Medicos.ServiceAPI.Domain;

namespace Medicos.ServiceAPI.Interfaces
{
    public interface IPacienteRepository
    {
        Task<bool> IsJaCadastradoAsync(string email, string cpf, long? id);
        Task InsertAsync(Paciente paciente);
        Task UpdateAsync(Paciente paciente);
        Task<Paciente?> FindByIdAsync(long id);
        Task DeleteByIdAsync(long id);
        IQueryable<Paciente> GetAll();
    }
}
