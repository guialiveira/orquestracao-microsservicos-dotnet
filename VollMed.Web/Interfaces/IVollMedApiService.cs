

using VollMed.Interfaces;
using VollMed.Web.Domain;
using VollMed.Web.Dtos;

namespace VollMed.Web.Interfaces
{
    public interface IVollMedApiService : IBaseHttpService
    {
        IVollMedApiService WithContext(HttpContext context);

        Task<PaginatedList<ConsultaDto>> ListarConsultas(int? page);
        Task<ConsultaDto> ObterFormularioConsulta(long? consultaId);
        Task ExcluirConsulta(long consultaId);
        Task<ConsultaDto> SalvarConsulta(ConsultaDto input);

        Task<PaginatedList<MedicoDto>> ListarMedicos(int? page);
        Task<MedicoDto> ObterFormularioMedico(long? medicoId);
        Task ExcluirMedico(long medicoId);
        Task<MedicoDto> SalvarMedico(MedicoDto input);
        Task<IEnumerable<MedicoDto>> ListarMedicosPorEspecialidade(Especialidade especEnum);

        Task<PaginatedList<PacienteDto>> ListarPacientes(int? page);
        Task<PacienteDto> ObterFormularioPaciente(long? pacienteId);
        Task ExcluirPaciente(long pacienteId);
        Task<PacienteDto> SalvarPaciente(PacienteDto input);
    }
}