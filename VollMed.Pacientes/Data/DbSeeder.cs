using Microsoft.EntityFrameworkCore;
using VollMed.Pacientes.Domain.Entities;

namespace VollMed.Pacientes.Data
{
    public static class DbSeeder
    {
        public static void Seed(VollMedDbContext context)
        {
            // Aplicar migrações pendentes
            context.Database.Migrate();

            // Verificar se já existem dados
            if (context.Pacientes.Any())
            {
                return; // Banco já possui dados
            }

            // Adicionar pacientes fictícios usando DTOs
            var pacientes = new List<Paciente>
            {
                new Paciente
                (
                    "Lucas Almeida",
                    "12345678901",
                    "lucas.almeida@email.com",
                    "(11) 91234-5678"
                ),
                new Paciente
                (
                    "Fernanda Lima",
                    "23456789012",
                    "fernanda.lima@email.com",
                    "(11) 92345-6789"
                ),
                new Paciente
                (
                    "Roberto Carvalho",
                    "34567890123",
                    "roberto.carvalho@email.com",
                    "(11) 93456-7890"
                ),
                new Paciente
                (
                    "Juliana Souza",
                    "45678901234",
                    "juliana.souza@email.com",
                    "(11) 94567-8901"
                ),
                new Paciente
                (
                    "Rafael Rodrigues",
                    "56789012345",
                    "rafael.rodrigues@email.com",
                    "(11) 95678-9012"
                ),
                new Paciente
                (
                    "Camila Ferreira",
                    "67890123456",
                    "camila.ferreira@email.com",
                    "(11) 96789-0123"
                ),
                new Paciente
                (
                    "Bruno Martins",
                    "78901234567",
                    "bruno.martins@email.com",
                    "(11) 97890-1234"
                ),
                new Paciente
                (
                    "Patricia Gomes",
                    "89012345678",
                    "patricia.gomes@email.com",
                    "(11) 98901-2345"
                )
            };

            context.Pacientes.AddRange(pacientes);
            context.SaveChanges();

        }
    }
    
}
