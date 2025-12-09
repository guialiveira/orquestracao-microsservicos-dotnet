using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VollMed.Consultas.Domain.Entities
{
    [Table("consultas")]
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string Paciente { get; private set; } = string.Empty;
        public long MedicoId { get; private set; }
        public DateTime Data { get; private set; }

        // Navigation property for FK (same database)
        public Medico? Medico { get; private set; }

        public Consulta() { }

        public Consulta(long medicoId, string paciente, DateTime data)
        {
            ModificarDados(medicoId, paciente, data);
        }

        public void ModificarDados(long medicoId, string paciente, DateTime data)
        {
            MedicoId = medicoId;
            Paciente = paciente;
            Data = data;
        }
    }
}
