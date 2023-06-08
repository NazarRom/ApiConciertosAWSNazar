using ApiConciertosAWSNazar.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosAWSNazar.Data
{
    public class ConciertosContext : DbContext
    {
        public ConciertosContext(DbContextOptions<ConciertosContext> options) : base(options) { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Evento> Eventos { get; set; }
    }
}
