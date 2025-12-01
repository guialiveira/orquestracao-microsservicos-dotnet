using Medicos.ServiceAPI.Domain;
using Medicos.ServiceAPI.Dto;
using Microsoft.EntityFrameworkCore;

namespace Medicos.ServiceAPI.Data
{
    public static class DbSeeder
    {
        public static void Seed(MedicosDbContext context)
        {
            // Aplicar migrações pendentes
            context.Database.Migrate();

            // Verificar se já existem dados
            if (context.Medicos.Any() || context.Pacientes.Any())
            {
                return; // Banco já possui dados
            }

            // Adicionar médicos fictícios usando DTOs
            var medicosDto = new List<MedicoDto>
            {
                new MedicoDto
                {
                    Nome = "Dr. João Silva",
                    Email = "joao.silva@vollmed.com",
                    Telefone = "(11) 98765-4321",
                    Crm = "123456",
                    Especialidade = Especialidade.Cardiologia
                },
                new MedicoDto
                {
                    Nome = "Dra. Maria Santos",
                    Email = "maria.santos@vollmed.com",
                    Telefone = "(11) 98765-4322",
                    Crm = "234567",
                    Especialidade = Especialidade.Pediatria
                },
                new MedicoDto
                {
                    Nome = "Dr. Pedro Costa",
                    Email = "pedro.costa@vollmed.com",
                    Telefone = "(11) 98765-4323",
                    Crm = "345678",
                    Especialidade = Especialidade.Neurocirurgia
                },
                new MedicoDto
                {
                    Nome = "Dra. Ana Oliveira",
                    Email = "ana.oliveira@vollmed.com",
                    Telefone = "(11) 98765-4324",
                    Crm = "456789",
                    Especialidade = Especialidade.Oncologia
                },
                new MedicoDto
                {
                    Nome = "Dr. Carlos Mendes",
                    Email = "carlos.mendes@vollmed.com",
                    Telefone = "(11) 98765-4325",
                    Crm = "567890",
                    Especialidade = Especialidade.CirurgiaGeral
                }
            };

            var medicos = medicosDto.Select(dto => new Medico(dto)).ToList();

            // Adicionar pacientes fictícios usando DTOs
            var pacientesDto = new List<PacienteDto>
            {
                new PacienteDto
                {
                    Nome = "Lucas Almeida",
                    Cpf = "12345678901",
                    Email = "lucas.almeida@email.com",
                    Telefone = "(11) 91234-5678"
                },
                new PacienteDto
                {
                    Nome = "Fernanda Lima",
                    Cpf = "23456789012",
                    Email = "fernanda.lima@email.com",
                    Telefone = "(11) 92345-6789"
                },
                new PacienteDto
                {
                    Nome = "Roberto Carvalho",
                    Cpf = "34567890123",
                    Email = "roberto.carvalho@email.com",
                    Telefone = "(11) 93456-7890"
                },
                new PacienteDto
                {
                    Nome = "Juliana Souza",
                    Cpf = "45678901234",
                    Email = "juliana.souza@email.com",
                    Telefone = "(11) 94567-8901"
                },
                new PacienteDto
                {
                    Nome = "Rafael Rodrigues",
                    Cpf = "56789012345",
                    Email = "rafael.rodrigues@email.com",
                    Telefone = "(11) 95678-9012"
                },
                new PacienteDto
                {
                    Nome = "Camila Ferreira",
                    Cpf = "67890123456",
                    Email = "camila.ferreira@email.com",
                    Telefone = "(11) 96789-0123"
                },
                new PacienteDto
                {
                    Nome = "Bruno Martins",
                    Cpf = "78901234567",
                    Email = "bruno.martins@email.com",
                    Telefone = "(11) 97890-1234"
                },
                new PacienteDto
                {
                    Nome = "Patricia Gomes",
                    Cpf = "89012345678",
                    Email = "patricia.gomes@email.com",
                    Telefone = "(11) 98901-2345"
                }
            };

            var pacientes = pacientesDto.Select(dto => new Paciente(dto)).ToList();

            context.Medicos.AddRange(medicos);
            context.Pacientes.AddRange(pacientes);
            context.SaveChanges();

            // Adicionar consultas fictícias usando DTOs
            var consultasDto = new List<ConsultaDto>
            {
                new ConsultaDto
                {
                    Paciente = "Lucas Almeida",
                    MedicoId = medicos[0].Id,
                    Data = DateTime.Now.AddDays(1)
                },
                new ConsultaDto
                {
                    Paciente = "Fernanda Lima",
                    MedicoId = medicos[1].Id,
                    Data = DateTime.Now.AddDays(2)
                },
                new ConsultaDto
                {
                    Paciente = "Roberto Carvalho",
                    MedicoId = medicos[2].Id,
                    Data = DateTime.Now.AddDays(3)
                },
                new ConsultaDto
                {
                    Paciente = "Juliana Souza",
                    MedicoId = medicos[3].Id,
                    Data = DateTime.Now.AddDays(4)
                },
                new ConsultaDto
                {
                    Paciente = "Rafael Rodrigues",
                    MedicoId = medicos[4].Id,
                    Data = DateTime.Now.AddDays(5)
                },
                new ConsultaDto
                {
                    Paciente = "Camila Ferreira",
                    MedicoId = medicos[0].Id,
                    Data = DateTime.Now.AddDays(6)
                },
                new ConsultaDto
                {
                    Paciente = "Bruno Martins",
                    MedicoId = medicos[1].Id,
                    Data = DateTime.Now.AddDays(7)
                }
            };

            var consultas = consultasDto.Select(dto => new Consulta(dto)).ToList();

            context.Consultas.AddRange(consultas);
            context.SaveChanges();
        }
    }
}
