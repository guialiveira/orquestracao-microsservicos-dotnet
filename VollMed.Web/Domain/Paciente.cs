using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VollMed.Web.Domain
{
    [Table("pacientes")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string? Nome { get; private set; }
        public string? Cpf { get; private set; }
        public string? Email { get; private set; }

        public string? Telefone { get; private set; }
    }
}
