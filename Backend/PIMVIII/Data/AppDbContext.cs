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
			modelBuilder.Entity<Playlist>()
			  .HasKey(p => p.ID);

			modelBuilder.Entity<Conteudo>()
				.HasKey(c => c.ID);

			modelBuilder.Entity<ItemPlaylist>()
				.HasKey(ip => new { ip.PlaylistID, ip.ConteudoID });

			modelBuilder.Entity<ItemPlaylist>()
				.HasOne(ip => ip.Playlist)
				.WithMany(p => p.Itens)
				.HasForeignKey(ip => ip.PlaylistID)
				.HasPrincipalKey(p => p.ID);

			modelBuilder.Entity<ItemPlaylist>()
				.HasOne(ip => ip.Conteudo)
				.WithMany()
				.HasForeignKey(ip => ip.ConteudoID)
				.HasPrincipalKey(p => p.ID);
		}
	}
}
