using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapchat.Domain.DTOs;

namespace Zapchat.Service.Validations
{
    public class UsuarioValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome).NotEmpty().WithMessage("O nome é obrigatório");
            RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
