using Zapchat.Domain.Entities;
using Zapchat.Domain.Interfaces;
using Zapchat.Service.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zapchat.Service.Interfaces;

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

        public async Task<IEnumerable<GrupoWhatsAppDto>> ListarTodos()
        {
            var grupos = await _repository.ListarTodos();
            return _mapper.Map<IEnumerable<GrupoWhatsAppDto>>(grupos);
        }

        public async Task<GrupoWhatsAppDto?> BuscarPorId(Guid id)
        {
            var grupo = await _repository.BuscarPorId(id);
            return _mapper.Map<GrupoWhatsAppDto>(grupo);
        }

        public async Task Adicionar(GrupoWhatsAppDto dto)
        {
            var grupo = _mapper.Map<GrupoWhatsApp>(dto);
            await _repository.Adicionar(grupo);
        }

        public async Task Atualizar(GrupoWhatsAppDto dto)
        {
            var grupo = _mapper.Map<GrupoWhatsApp>(dto);
            await _repository.Atualizar(grupo);
        }

        public async Task Deletar(Guid id) => await _repository.Deletar(id);
    }
}
