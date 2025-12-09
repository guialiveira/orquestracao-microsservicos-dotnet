using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VollMed.Medicos.Domain.Enums;

namespace VollMed.Medicos.Domain.Entities
{
    [Table("medicos")]
    public class Medico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string Nome { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string Telefone { get; private set; } = string.Empty;

        public string Crm { get; private set; } = string.Empty;

        public Especialidade Especialidade { get; private set; }

        // Note: Consultas navigation property kept for FK integrity (same database)
        // Not used directly in this service but maintains relationship
        public ICollection<Consulta> Consultas { get; private set; } = new List<Consulta>();

        public Medico() { }

        public Medico(string nome, string email, string telefone, string crm, Especialidade especialidade)
        {
            AtualizarDados(nome, email, telefone, crm, especialidade);
        }

        public void AtualizarDados(string nome, string email, string telefone, string crm, Especialidade especialidade)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Crm = crm;
            Especialidade = especialidade;
        }
    }

    // Placeholder for Consulta to maintain FK relationship (same database)
    [Table("consultas")]
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }
        public string Paciente { get; private set; } = string.Empty;
        public long MedicoId { get; private set; }
        public DateTime Data { get; private set; }
    }
}
