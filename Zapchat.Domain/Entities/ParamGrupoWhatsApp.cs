using System;

namespace Zapchat.Domain.Entities
{
    public class ParamGrupoWhatsApp : Entity
    {
        public Guid GrupoId { get; set; }
        public string AppKey { get; set; } = string.Empty;
        public string AppSecret { get; set; } = string.Empty;
    }
}
