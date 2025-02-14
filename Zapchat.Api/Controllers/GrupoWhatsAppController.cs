using Microsoft.AspNetCore.Mvc;
using Zapchat.Domain.Interfaces;
using Zapchat.Domain.DTOs;

namespace Zapchat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoWhatsAppController : ControllerBase
    {
        private readonly IGrupoWhatsAppService _grupoWhatsAppservice;

        public GrupoWhatsAppController(IGrupoWhatsAppService grupoWhatsAppservice)
        {
            _grupoWhatsAppservice = grupoWhatsAppservice;
        }

        [HttpGet]
        public async Task<IEnumerable<GrupoWhatsAppDto>> ListarTodos() => await _grupoWhatsAppservice.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var grupo = await _grupoWhatsAppservice.GetByIdAsync(id);
            if (grupo == null)
                return NotFound();

            return Ok(grupo);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GrupoWhatsAppDto dto)
        {
            try
            {
                var grupoWhats = await _grupoWhatsAppservice.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = grupoWhats.Id }, grupoWhats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] GrupoWhatsAppDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _grupoWhatsAppservice.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _grupoWhatsAppservice.DeleteAsync(id);
            return NoContent();
        }
    }
}
