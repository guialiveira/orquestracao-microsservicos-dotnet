using VollMed.Consultas.Domain.Entities;

namespace VollMed.Consultas.Domain.Interfaces
{
    public interface IConsultaRepository
    {
        IQueryable<Consulta> GetAllOrderedByData();
        Task SaveAsync(Consulta consulta);
        Task<Consulta?> FindByIdAsync(long id);
        Task DeleteByIdAsync(long id);
        Task UpdateAsync(Consulta consulta);
    }
}
