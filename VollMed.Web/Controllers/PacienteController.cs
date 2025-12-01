using VollMed.Web.Dtos;
using VollMed.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VollMed.Web.Controllers
{
    [Route("pacientes")]
    public class PacienteController : BaseController
    {
        private const string PaginaListagem = "Listagem";
        private const string PaginaCadastro = "Formulario";
        private readonly IVollMedApiService _vollMedApiService;

        public PacienteController(IVollMedApiService vollMedApiService)
        : base()
        {
            _vollMedApiService = vollMedApiService;
        }

        [HttpGet]
        [Route("{page?}")]
        public async Task<IActionResult> ListarAsync([FromQuery] int page = 1)
        {
            var pacientes = await _vollMedApiService
                .WithContext(HttpContext)
                .ListarPacientes(page);
            ViewBag.Pacientes = pacientes;
            ViewData["Url"] = "Pacientes";
            return View(PaginaListagem, pacientes);
        }

        [HttpGet]
        [Route("formulario/{id?}")]
        public async Task<IActionResult> ObterFormularioAsync(long? id = 0)
        {
            PacienteDto paciente = await _vollMedApiService
                .WithContext(HttpContext)
                .ObterFormularioPaciente(id);
            return View(PaginaCadastro, paciente);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SalvarAsync([FromForm] PacienteDto dados)
        {
            if (dados._method == "delete")
            {
                if (dados.Id.HasValue)
                {
                    await _vollMedApiService
                        .WithContext(HttpContext)
                        .ExcluirPaciente(dados.Id.Value);
                }
                return Redirect("/pacientes");
            }

            if (!ModelState.IsValid)
            {
                return View(PaginaCadastro, dados);
            }

            try
            {
                await _vollMedApiService
                    .WithContext(HttpContext)
                    .SalvarPaciente(dados);
                return Redirect("/pacientes");
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
                ViewBag.Dados = dados;
                return View(PaginaCadastro);
            }
        }
    }
}
