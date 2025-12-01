using Medicos.ServiceAPI.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicos.ServiceAPI.Domain
{
    [Table("consultas")]
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string Paciente { get; private set; }

        public long MedicoId { get; private set; }

        public DateTime Data { get; private set; }

        public Consulta() { }

        public Consulta(ConsultaDto dados)
        {
            ModificarDados(dados);
        }

        public void ModificarDados(ConsultaDto dados)
        {
            MedicoId = dados.MedicoId;
            Paciente = dados.Paciente;
            Data = dados.Data;
        }
    }
}
