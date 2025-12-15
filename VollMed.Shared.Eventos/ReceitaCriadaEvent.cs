namespace VollMed.Shared.Eventos
{
    public class ReceitaCriadaEvent
    {
        public long ConsultaId { get; set; }
        public long PacienteId { get; set; }
        public long MedicoId { get; set; }
        public required string Receita { get; set; }
    }
}
