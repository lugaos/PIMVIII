using Microsoft.EntityFrameworkCore;
using PIMVIII.Models;

namespace PIMVIII.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Criador> Criador { get; set; }
        public DbSet<Conteudo> Conteudo { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<ItemPlaylist> ItemPlaylist { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemPlaylist>()
                .HasKey(item => new { item.PlaylistID, item.ConteudoID });
        }
    }
}
