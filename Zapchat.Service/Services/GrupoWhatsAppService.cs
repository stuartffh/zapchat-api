using Zapchat.Domain.Interfaces;
using AutoMapper;
using Zapchat.Domain.DTOs;

namespace Zapchat.Service.Services
{
    public class GrupoWhatsAppService : IGrupoWhatsAppService
    {
        private readonly IGrupoWhatsAppRepository _repository;
        private readonly IMapper _mapper;

        public GrupoWhatsAppService(IGrupoWhatsAppRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<GrupoWhatsAppDto> AddAsync(GrupoWhatsAppDto usuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GrupoWhatsAppDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GrupoWhatsAppDto?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GrupoWhatsAppDto?> UpdateAsync(Guid id, GrupoWhatsAppDto usuarioDto)
        {
            throw new NotImplementedException();
        }
    }
}
