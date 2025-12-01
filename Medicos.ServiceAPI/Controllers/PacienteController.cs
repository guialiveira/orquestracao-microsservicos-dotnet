using Medicos.ServiceAPI.Dto;
using Medicos.ServiceAPI.Exceptions;
using Medicos.ServiceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medicos.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _service;

        public PacienteController(IPacienteService service)
        {
            _service = service;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarAsync([FromQuery] int page = 1)
        {
            var pacientesCadastrados = await _service.ListarAsync(page);
            return Ok(pacientesCadastrados);
        }

        [HttpGet("formulario/{id?}")]
        public async Task<IActionResult> ObterFormularioAsync(long id = 0)
        {
            var dados = id > 0
                ? await _service.CarregarPorIdAsync(id)
                : new PacienteDto();

            return Ok(dados);
        }

        [HttpPut("Salvar")]
        [HttpPost("Salvar")]
        public async Task<IActionResult> SalvarAsync([FromBody] PacienteDto dados)
        {
            try
            {
                await _service.CadastrarAsync(dados);
                return Ok(dados);
            }
            catch (RegraDeNegocioException ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> ExcluirAsync(int id)
        {
            await _service.ExcluirAsync(id);
            return Ok();
        }
    }
}
