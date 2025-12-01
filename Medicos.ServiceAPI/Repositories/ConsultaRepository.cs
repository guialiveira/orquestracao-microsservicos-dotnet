using Medicos.ServiceAPI.Data;
using Medicos.ServiceAPI.Domain;
using Medicos.ServiceAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medicos.ServiceAPI.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly MedicosDbContext _context;

        public ConsultaRepository(MedicosDbContext context)
        {
            _context = context;
        }

        public IQueryable<Consulta> GetAllOrderedByData()
        {
            try
            {
                var ret = _context.Consultas.OrderBy(c => c.Data);
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task SaveAsync(Consulta consulta)
        {
            _context.Add(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Consulta consulta)
        {
            _context.Update(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<Consulta> FindByIdAsync(long id)
        {
            return await _context.Consultas.SingleAsync(c => c.Id == id);
        }

        public async Task DeleteByIdAsync(long id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
