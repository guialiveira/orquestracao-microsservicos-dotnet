using VollMed.Web.Dtos;
using VollMed.Web.Interfaces;
using VollMed.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using VollMed.Interfaces;
using VollMed.Web.Domain;

namespace VollMed.Web.Services
{
    public class VollMedApiService : BaseHttpService, IVollMedApiService
    {
        public override string Scope => throw new NotImplementedException();

        public VollMedApiService(IConfiguration configuration, HttpClient httpClient)
            : base(configuration, httpClient)
        {
            _baseUri = _configuration["Medicos.ServiceAPI.Url"]!;
        }

        public IVollMedApiService WithContext(HttpContext context)
        {
            _httpContext = context;
            return this;
        }

        #region Consulta

        public async Task<PaginatedList<ConsultaDto>> ListarConsultas(int? page)
        {
            var url = $"{_baseUri}/consulta/listar?page={page ?? 1}";
            return await _httpClient.GetFromJsonAsync<PaginatedList<ConsultaDto>>(url)
                   ?? new PaginatedList<ConsultaDto>(new List<ConsultaDto>(), 0, page ?? 1, 10);
        }

        public async Task<ConsultaDto> ObterFormularioConsulta(long? consultaId)
        {
            var url = $"{_baseUri}/consulta/formulario/{consultaId}";
            return await _httpClient.GetFromJsonAsync<ConsultaDto>(url) ?? new ConsultaDto();
        }

        public async Task<ConsultaDto> SalvarConsulta(ConsultaDto input)
        {
            var url = $"{_baseUri}/consulta/salvar";
            var response = await _httpClient.PostAsJsonAsync(url, input);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao salvar consulta: {erro}");
            }

            return await response.Content.ReadFromJsonAsync<ConsultaDto>();
        }

        public async Task ExcluirConsulta(long consultaId)
        {
            try
            {
                var url = $"{_baseUri}/consulta/excluir";
                var response = await _httpClient.DeleteAsync($"{url}/{consultaId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Medico

        public async Task<MedicoDto> ObterFormularioMedico(long? medicoId)
        {
            var url = $"{_baseUri}/Medico/formulario/{medicoId}";
            return await _httpClient.GetFromJsonAsync<MedicoDto>(url) ?? new MedicoDto();
        }
        public async Task<IEnumerable<MedicoDto>> ListarMedicosPorEspecialidade(Especialidade especEnum)
        {
            var url = $"{_baseUri}/medico/especialidade/{(int)especEnum}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<MedicoDto>>(url)
                   ?? Enumerable.Empty<MedicoDto>();
        }
        public async Task<PaginatedList<MedicoDto>> ListarMedicos(int? page)
        {
            try
            {

                var url = $"{_baseUri}/medico/listar?page={page ?? 1}";
                var medicos = await _httpClient.GetFromJsonAsync<PaginatedList<MedicoDto>>(url);
                return await _httpClient.GetFromJsonAsync<PaginatedList<MedicoDto>>(url)
                       ?? new PaginatedList<MedicoDto>(new List<MedicoDto>(), 0, page ?? 1, 10);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw new NotImplementedException();
            }
        }
        public async Task<MedicoDto> SalvarMedico(MedicoDto input)
        {
            var url = $"{_baseUri}/medico/salvar";
            var response = await _httpClient.PostAsJsonAsync(url, input);
            return await response.Content.ReadFromJsonAsync<MedicoDto>();
        }
        public Task ExcluirMedico(long medicoId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Paciente

        //public async Task<PacienteDto> ObterFormularioPaciente(long? pacienteId, string pacienteCpf)
        //{
        //    if(!string.IsNullOrEmpty(pacienteCpf))
        //        return await ObterPacientePorCpf(pacienteCpf);

        //    return await ObterPaciente(pacienteId);
        //}

        //private async Task<PacienteDto> ObterPaciente(long? pacienteId)
        //{
        //    var url = $"{_baseUri}/Paciente/formulario/{pacienteId}";
        //    return await _httpClient.GetFromJsonAsync<PacienteDto>(url) ?? new PacienteDto();
        //}

        //private async Task<PacienteDto?> ObterPacientePorCpf(string pacienteCpf)
        //{
        //    var url = $"{_baseUri}/Paciente/formularioporcpf/{pacienteCpf}";
        //    var response = await _httpClient.GetAsync(url);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadFromJsonAsync<PacienteDto>();
        //    }

        //    // opcional: lê o erro retornado pela API
        //    var erro = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine($"Erro ao buscar paciente: {erro}");

        //    return null; // ou new PacienteDto(), dependendo da lógica
        //}

        //private async Task<PacienteDto> SalvarPaciente(PacienteDto input)
        //{
        //    var url = $"{_baseUri}/paciente/salvar";
        //    var response = await _httpClient.PostAsJsonAsync(url, input);
        //    return await response.Content.ReadFromJsonAsync<PacienteDto>();
        //}


        #endregion

    }
}
