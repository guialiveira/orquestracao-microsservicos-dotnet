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
        public long PacienteId { get; set; }
        public string PacienteNome { get; private set; } = string.Empty;
        public long MedicoId { get; private set; }
        public string MedicoNome { get; set; }
        public DateTime Data { get; set; }
        public string Receita { get; set; }

        public Consulta() { }

        public Consulta(long medicoId, string medicoNome, long pacienteId, string pacienteNome, DateTime data)
        {
            ModificarDados(medicoId, medicoNome, pacienteId, pacienteNome, data);
        }

        public void ModificarDados(long medicoId, string medicoNome, long pacienteId, string pacienteNome, DateTime data)
        {
            MedicoId = medicoId;
            MedicoNome = medicoNome;
            PacienteId = pacienteId;
            PacienteNome = pacienteNome;
            Data = data;
        }
    }
}
