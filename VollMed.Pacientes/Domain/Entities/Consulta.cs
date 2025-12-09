using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VollMed.Pacientes.Domain.Entities
{
    // Placeholder entity for shared database context
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
