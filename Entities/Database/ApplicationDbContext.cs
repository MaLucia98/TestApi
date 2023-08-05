using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Sentidos> Sentidos { get; set; }
        public DbSet<Estaciones> Estaciones { get; set; }
    }
}