using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VollMed.Consultas.Domain.Enums;

namespace VollMed.Consultas.Domain.Entities
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

        public ICollection<Consulta> Consultas { get; private set; } = new List<Consulta>();
    }
}
