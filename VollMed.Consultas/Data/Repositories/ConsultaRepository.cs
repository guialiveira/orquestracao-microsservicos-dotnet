using Microsoft.EntityFrameworkCore;
using VollMed.Consultas.Domain.Entities;
using VollMed.Consultas.Domain.Interfaces;

namespace VollMed.Consultas.Data.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly VollMedDbContext _context;

        public ConsultaRepository(VollMedDbContext context)
        {
            _context = context;
        }

        public IQueryable<Consulta> GetAllOrderedByData()
        {
            return _context.Consultas
                .Include(c => c.Medico)
                .OrderBy(c => c.Data)
                .AsQueryable();
        }

        public async Task SaveAsync(Consulta consulta)
        {
            _context.Add(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<Consulta?> FindByIdAsync(long id)
        {
            return await _context.Consultas
                .Include(c => c.Medico)
                .SingleOrDefaultAsync(c => c.Id == id);
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

        public async Task UpdateAsync(Consulta consulta)
        {
            _context.Update(consulta);
            await _context.SaveChangesAsync();
        }
    }
}
