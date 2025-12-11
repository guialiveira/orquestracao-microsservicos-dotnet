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

        public async Task InsertRangeAsync(IEnumerable<Paciente> pacientes, Action<int> onProgress)
        {
            // Inicia transação com isolation level SERIALIZABLE para demonstrar locks no SQL Server
            // SERIALIZABLE é o nível mais restritivo e garante lock exclusivo durante toda a transação
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                // Adquire lock exclusivo na tabela Pacientes (equivalente ao BEGIN EXCLUSIVE do SQLite)
                // TABLOCKX garante que nenhuma outra transação pode ler ou escrever na tabela
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

                        onProgress(count);  // Callback para logging

                        // Lock MANTIDO durante o sleep (transação ainda aberta)
                        Thread.Sleep(5000);

                        batch.Clear();
                    }
                }

                // Salvar último batch se houver
                if (batch.Count > 0)
                {
                    _context.Pacientes.AddRange(batch);
                    await _context.SaveChangesAsync();
                    onProgress(count);
                }

                // Commit da transação (libera todos os locks)
                await transaction.CommitAsync();
            }
            catch
            {
                // Rollback em caso de erro (libera todos os locks)
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
