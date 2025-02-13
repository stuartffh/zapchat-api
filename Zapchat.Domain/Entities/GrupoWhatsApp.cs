using System;
namespace Zapchat.Domain.Entities
{
    public class GrupoWhatsApp : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string Identificador { get; set; } = string.Empty;
        public string? Descricao { get; set; }
    }
}
