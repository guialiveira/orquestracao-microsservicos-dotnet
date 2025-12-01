using VollMed.Web.Dtos;
using VollMed.Web.Exceptions;
using VollMed.Web.Interfaces;
using VollMed.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace VollMed.Web.Controllers
{
    [Route("consultas")]
    public class ConsultaController : BaseController
    {
        private const string PaginaListagem = "Listagem";
        private const string PaginaCadastro = "Formulario";

        private readonly IConsultaService _consultaservice;
        private readonly IVollMedApiService _vollMedApiService;

        public ConsultaController(IConsultaService consultaService, IVollMedApiService vollMedApiService)
        {
            _consultaservice = consultaService;
            _vollMedApiService = vollMedApiService;
        }

        [HttpGet]
        [Route("{page?}")]
        public async Task<IActionResult> ListarAsync([FromQuery] int page = 1)
        {
            var consultasAtivas = await _consultaservice.ListarAsync(page);
            ViewBag.Consultas = consultasAtivas;
            ViewData["Url"] = "Consultas";
            return View(PaginaListagem, consultasAtivas);
        }

        [HttpGet]
        [Route("formulario/{id?}")]
        public async Task<IActionResult> ObterFormularioAsync(long? id)
        {
            var dados = id.HasValue
                ? await _consultaservice.CarregarPorIdAsync(id.Value)
                : new ConsultaDto { Data = DateTime.Now };

            // Carregar todas as especialidades disponíveis
            ViewData["Especialidades"] = Enum.GetValues(typeof(Especialidade)).Cast<Especialidade>().ToList();

            PaginatedList<MedicoDto> medicos = await _vollMedApiService.WithContext(HttpContext).ListarMedicos(1);
            ViewData["Medicos"] = medicos.Items;
            PaginatedList<PacienteDto> pacientes = await _vollMedApiService.WithContext(HttpContext).ListarPacientes(1);
            ViewData["Pacientes"] = pacientes.Items;
            return View(PaginaCadastro, dados);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SalvarAsync([FromForm] ConsultaDto dados)
        {
            if (dados._method == "delete")
            {
                await _consultaservice.ExcluirAsync(dados.Id);
                return Redirect("/consultas");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Especialidades"] = Enum.GetValues(typeof(Especialidade)).Cast<Especialidade>().ToList();
                PaginatedList<MedicoDto> medicos = await _vollMedApiService.WithContext(HttpContext).ListarMedicos(1);
                ViewData["Medicos"] = medicos.Items;
                PaginatedList<PacienteDto> pacientes = await _vollMedApiService.WithContext(HttpContext).ListarPacientes(1);
                ViewData["Pacientes"] = pacientes.Items;
                return View(PaginaCadastro, dados);
            }

            try
            {
                await _consultaservice.CadastrarAsync(dados);
                return Redirect("/consultas");
            }
            catch (RegraDeNegocioException ex)
            {
                ViewBag.Erro = ex.Message;
                ViewBag.Dados = dados;
                return View(PaginaCadastro);
            }
        }
    }
}
