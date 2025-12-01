using Medicos.ServiceAPI.Dto;
using Medicos.ServiceAPI.Exceptions;
using Medicos.ServiceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medicos.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _service;

        public ConsultaController(IConsultaService service)
        {
            _service = service;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarAsync([FromQuery] int page = 1)
        {
            var consultasCadastradas = await _service.ListarAsync(page);

            return Ok(consultasCadastradas);
        }

        [HttpGet("formulario/{id?}")]
        public async Task<IActionResult> ObterFormularioAsync(long id = 0)
        {
            var dados = id > 0
                ? await _service.CarregarPorIdAsync(id)
                : new ConsultaDto();

            return Ok(dados);
        }

        [HttpPut("Salvar")]
        [HttpPost("Salvar")]
        public async Task<IActionResult> SalvarAsync([FromBody] ConsultaDto dados)
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
