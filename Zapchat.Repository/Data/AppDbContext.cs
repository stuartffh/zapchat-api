using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zapchat.Domain.Entities;

namespace Zapchat.Repository.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options) 
        { 
            _configuration = configuration;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<GrupoWhatsApp> GruposWhatsApp { get; set; }
        public DbSet<AdmGrupoWhatsApp> AdmsGrupoWhatsApp { get; set; }
        public DbSet<ParamGrupoWhatsApp> ParamsGrupoWhatsApp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlite(connectionString);
            }
        }

    }
}
