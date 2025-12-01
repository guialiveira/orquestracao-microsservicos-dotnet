using Medicos.ServiceAPI.Data;
using Medicos.ServiceAPI.Domain;
using Medicos.ServiceAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medicos.ServiceAPI.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly MedicosDbContext _context;

        public PacienteRepository(MedicosDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsJaCadastradoAsync(string email, string cpf, long? id)
        {
            return await _context.Pacientes
                .AnyAsync(p => (p.Email == email || p.Cpf == cpf) && (!id.HasValue || p.Id != id));
        }

        public async Task InsertAsync(Paciente paciente)
        {
            _context.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Paciente paciente)
        {
            _context.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task<Paciente?> FindByIdAsync(long id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task DeleteByIdAsync(long id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Paciente> GetAll()
        {
            return _context.Pacientes.AsQueryable();
        }
    }
}
