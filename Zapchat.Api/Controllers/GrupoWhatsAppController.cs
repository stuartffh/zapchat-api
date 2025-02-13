using Microsoft.AspNetCore.Mvc;
using Zapchat.Service.DTOs;
using Zapchat.Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zapchat.Service.Interfaces;
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
        public async Task<IEnumerable<GrupoWhatsAppDto>> ListarTodos() => await _grupoWhatsAppservice.ListarTodos();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var grupo = await _grupoWhatsAppservice.BuscarPorId(id);
            if (grupo == null)
                return NotFound();

            return Ok(grupo);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GrupoWhatsAppDto dto)
        {
            try
            {
                var grupoWhats = await _grupoWhatsAppservice.Adicionar(dto);
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
            await _grupoWhatsAppservice.Atualizar(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _grupoWhatsAppservice.Deletar(id);
            return NoContent();
        }
    }
}
