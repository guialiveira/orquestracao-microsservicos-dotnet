using VollMed.Web.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace VollMed.Web.Dtos
{
    public class ConsultaDto
    {

        public long Id { get; set; }
        public string _method { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public long MedicoId { get; set; }
        [ValidateNever]
        public string MedicoNome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório"), StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 digitos")]
        public string Paciente { get; set; }
        [ValidateNever]
        public string? PacienteNome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório"), DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public Especialidade Especialidade { get; set; }

        public ConsultaDto()
        {
        }

        public ConsultaDto(
            long Id,
            long MedicoId,
            string MedicoNome,
            string Paciente,
            string? PacienteNome,
            DateTime Data,
            Especialidade Especialidade
        )
        {
            this.Id = Id;
            this.MedicoId = MedicoId;
            this.MedicoNome = MedicoNome;
            this.Paciente = Paciente;
            this.PacienteNome = PacienteNome;
            this.Data = Data;
            this.Especialidade = Especialidade;
        }

        public ConsultaDto(Consulta consulta)
        {
            Id = consulta.Id;
            MedicoId = consulta.MedicoId;
            
            Paciente = consulta.Paciente;
            Data = consulta.Data;
            
        }


    }
}


