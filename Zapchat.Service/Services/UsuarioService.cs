using AutoMapper;
using FluentValidation;
using Zapchat.Domain.DTOs;
using Zapchat.Domain.Entities;
using Zapchat.Domain.Interfaces;

namespace Zapchat.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IValidator<UsuarioDto> _validator;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IValidator<UsuarioDto> validator, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null ? _mapper.Map<UsuarioDto>(usuario) : null;
        }

        public async Task<UsuarioDto> AddAsync(UsuarioDto usuarioDto)
        {
            var validationResult = await _validator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            await _usuarioRepository.AddAsync(usuario);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto?> UpdateAsync(int id, UsuarioDto usuarioDto)
        {
            var validationResult = await _validator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExistente == null) return null;

            var usuarioAtualizado = _mapper.Map(usuarioDto, usuarioExistente);
            await _usuarioRepository.UpdateAsync(usuarioAtualizado);

            return _mapper.Map<UsuarioDto>(usuarioAtualizado);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return false;

            await _usuarioRepository.DeleteAsync(id);
            return true;
        }
    }
}
