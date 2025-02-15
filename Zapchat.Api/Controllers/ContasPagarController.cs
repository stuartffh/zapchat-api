using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Zapchat.Domain.DTOs.ContasPagar;
using Zapchat.Domain.Interfaces.ContasPagar;
using Zapchat.Service.Services;

namespace Zapchat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasPagarController : ControllerBase
    {
        private readonly IContasPagarService _contasPagarService;

        public ContasPagarController(IContasPagarService contasPagarService)
        {
            _contasPagarService = contasPagarService;
        }

        [HttpPost]
        public async Task<IActionResult> ListarContasPagarExcel([FromBody] ListarContasPagarExcelDto listarContaDto)
        {
            try
            {
                var base64 = await _contasPagarService.ListarContasPagarExcel(listarContaDto);
                return Ok(base64);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
