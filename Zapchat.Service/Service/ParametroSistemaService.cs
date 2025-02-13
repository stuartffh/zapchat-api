using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapchat.Domain.DTOs;
using Zapchat.Domain.Interfaces;
using Zapchat.Service.Interfaces;

namespace Zapchat.Service.Service
{
    public class ParametroSistemaService : IParametroSistemaService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IValidator<UsuarioDto> _validator;
        private readonly IMapper _mapper;

        public ParametroSistemaService(IUsuarioRepository usuarioRepository, IValidator<UsuarioDto> validator, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _validator = validator;
            _mapper = mapper;
        }
    }
}
