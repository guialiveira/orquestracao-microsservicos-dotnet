using Microsoft.EntityFrameworkCore;
using VollMed.Medicos.Domain.Entities;
using VollMed.Medicos.Domain.Enums;

namespace VollMed.Medicos.Data
{
    public static class DbSeeder
    {
        public static void Seed(VollMedDbContext context)
        {
            // Aplicar migrações pendentes
            context.Database.Migrate();

            // Verificar se já existem dados
            if (context.Medicos.Any())
            {
                return; // Banco já possui dados
            }

            // Adicionar médicos fictícios usando DTOs
            var medicos = new List<Medico>
            {
                new Medico
                (
                    nome: "Dr. João Silva",
                    email: "joao.silva@vollmed.com",
                    telefone: "(11) 98765-4321",
                    crm: "123456",
                    especialidade: Especialidade.Cardiologia
                ),
                new Medico
                (
                    "Dra. Maria Santos",
                    "maria.santos@vollmed.com",
                    "(11) 98765-4322",
                    "234567",
                    Especialidade.Pediatria
                ),
                new Medico
                (
                    "Dr. Pedro Costa",
                    "pedro.costa@vollmed.com",
                    "(11) 98765-4323",
                    "345678",
                    Especialidade.Neurocirurgia
                ),
                new Medico
                (
                    "Dra. Ana Oliveira",
                    "ana.oliveira@vollmed.com",
                    "(11) 98765-4324",
                    "456789",
                    Especialidade.Oncologia
                ),
                new Medico
                (
                    "Dr. Carlos Mendes",
                    "carlos.mendes@vollmed.com",
                    "(11) 98765-4325",
                    "567890",
                    Especialidade.CirurgiaGeral
                )
            };

            context.Medicos.AddRange(medicos);
            context.SaveChanges();
        }
    }
}
