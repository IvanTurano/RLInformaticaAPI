using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RLInformaticaAPI.Entidades;

namespace RLInformaticaAPI
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Marca> Marcas {  get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Reparacion> Reparaciones { get; set; }
    }
}
