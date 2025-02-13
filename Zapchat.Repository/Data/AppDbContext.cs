using Microsoft.EntityFrameworkCore;
using Zapchat.Domain.Entities;

namespace Zapchat.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<GrupoWhatsApp> GruposWhatsApp { get; set; }
        public DbSet<AdmGrupoWhatsApp> AdmsGrupoWhatsApp { get; set; }
        public DbSet<ParamGrupoWhatsApp> ParamsGrupoWhatsApp { get; set; }

    }
}
