using Zapchat.Domain.DTOs;

namespace Zapchat.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto?> GetByIdAsync(int id);
        Task<UsuarioDto> AddAsync(UsuarioDto usuarioDto);
        Task<UsuarioDto?> UpdateAsync(int id, UsuarioDto usuarioDto);
        Task<bool> DeleteAsync(int id);
    }
}
