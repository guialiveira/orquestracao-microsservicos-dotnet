using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VollMed.Medicos.Domain.Entities
{
    // Placeholder entity for shared database context
    // This service doesn't interact with Pacientes, but they share the same database
    [Table("pacientes")]
    public class Paciente
    {
        public Paciente(string nome, string cpf, string email, string telefone)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Telefone { get; private set; } = string.Empty;

    }
}
