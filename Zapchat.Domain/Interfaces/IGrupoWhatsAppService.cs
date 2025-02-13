using Zapchat.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zapchat.Domain.DTOs;

namespace Zapchat.Service.Interfaces
{
    public interface IGrupoWhatsAppService
    {
        Task<IEnumerable<GrupoWhatsAppDto>> ListarTodos();
        Task<GrupoWhatsAppDto?> BuscarPorId(Guid id);
        Task<GrupoWhatsAppDto> Adicionar(GrupoWhatsAppDto dto);
        Task Atualizar(GrupoWhatsAppDto dto);
        Task Deletar(Guid id);
    }
}
