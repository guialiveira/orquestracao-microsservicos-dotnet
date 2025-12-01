using Medicos.ServiceAPI.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicos.ServiceAPI.Domain
{
    [Table("pacientes")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }

        public Paciente() { }

        public Paciente(PacienteDto dados)
        {
            AtualizarDados(dados);
        }

        public void AtualizarDados(PacienteDto dados)
        {
            Nome = dados.Nome;
            Cpf = dados.Cpf;
            Email = dados.Email;
            Telefone = dados.Telefone;
        }
    }
}
