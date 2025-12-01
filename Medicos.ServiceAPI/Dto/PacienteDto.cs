using Medicos.ServiceAPI.Domain;
using System.ComponentModel.DataAnnotations;

namespace Medicos.ServiceAPI.Dto
{
    public class PacienteDto
    {

        public long? Id { get; set; }
        public string _method { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), MinLength(1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), RegularExpression(@"	^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$",
            ErrorMessage = "Cpf deve ter de 11 digitos numéricos")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), RegularExpression(@"^(?:\d{8}|\d{9}|\d{4}-\d{4}|\d{5}-\d{4}|\(\d{2}\)\s*\d{4}-\d{4}|\(\d{2}\)\s*\d{5}-\d{4}|\(\d{2}\)\s*\d{9})$",
            ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }


        public PacienteDto()
        {
        }

        public PacienteDto(Paciente paciente)
        {
            Id = paciente.Id;
            Nome = paciente.Nome;
            Email = paciente.Email;
            Telefone = paciente.Telefone;
            Cpf = paciente.Cpf;
        }
    }
}
