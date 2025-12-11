using Microsoft.EntityFrameworkCore;
using VollMed.Pacientes.Domain.Entities;
using VollMed.Pacientes.Domain.Interfaces;

namespace VollMed.Pacientes.Data.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly VollMedDbContext _context;

        public PacienteRepository(VollMedDbContext context)
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

        public async Task InsertRangeAsync(IEnumerable<Paciente> pacientes, Action<int> onProgress)
        {
            // Transaction with SERIALIZABLE isolation to demonstrate locks
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                // Acquire exclusive table lock (TABLOCKX)
                await _context.Database.ExecuteSqlRawAsync("SELECT TOP 0 * FROM pacientes WITH (TABLOCKX)");

                int count = 0;
                int batchSize = 500;
                var batch = new List<Paciente>();

                foreach (var paciente in pacientes)
                {
                    batch.Add(paciente);
                    count++;

                    if (batch.Count >= batchSize)
                    {
                        _context.Pacientes.AddRange(batch);
                        await _context.SaveChangesAsync();

                        onProgress(count);

                        // Lock maintained during sleep (transaction still open)
                        Thread.Sleep(5000);

                        batch.Clear();
                    }
                }

                // Save last batch if any
                if (batch.Count > 0)
                {
                    _context.Pacientes.AddRange(batch);
                    await _context.SaveChangesAsync();
                    onProgress(count);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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

        public Task DeleteAllAsync()
        {
            return _context.Pacientes.ExecuteDeleteAsync();
        }
    }
}
