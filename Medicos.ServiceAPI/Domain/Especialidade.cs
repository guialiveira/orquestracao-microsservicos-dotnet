using System.ComponentModel.DataAnnotations;

namespace Medicos.ServiceAPI.Domain
{
    public enum Especialidade
    {
        [Display(Name = "Clínica Geral")]
        ClinicaGeral = 0,

        [Display(Name = "Cardiologia")]
        Cardiologia = 1,

        [Display(Name = "Neurocirurgia")]
        Neurocirurgia = 2,

        [Display(Name = "Cirurgia Geral")]
        CirurgiaGeral = 3,

        [Display(Name = "Pediatria")]
        Pediatria = 4,

        [Display(Name = "Oncologia")]
        Oncologia = 5,

        [Display(Name = "Diagnóstico")]
        Diagnostico = 6
    }
}
