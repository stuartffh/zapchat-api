using AutoMapper;
using Zapchat.Domain.Entities;
using Zapchat.Domain.DTOs;

namespace Zapchat.Service.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
                .ReverseMap();
        }
    }
}
