using System;

namespace Zapchat.Domain.Entities
{
    public class AdmGrupoWhatsApp : Entity
    {
        public Guid GrupoId { get; set; }
        public string NumeroAdm { get; set; } = string.Empty;
    }
}
