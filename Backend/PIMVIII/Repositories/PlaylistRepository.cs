using Microsoft.EntityFrameworkCore;
using PIMVIII.Data;
using PIMVIII.Models;

namespace PIMVIII.Repositories
{
	public class PlaylistRepository : IPlaylistRepository
	{
		private readonly AppDbContext _context;

		public PlaylistRepository(AppDbContext context)
		{
			_context = context;
		}

		public void AddPlaylist(Playlist playlist)
		{
			_context.Playlist.Add(playlist);
			_context.SaveChanges();
		}

		public List<Playlist> GetAllPlaylists()
		{
			var itens = _context.ItemPlaylist.ToList();

			var playlists = _context.Playlist
				.Include(u => u.Usuario)
				.Include(i => i.Itens)
					.ThenInclude(i => i.Conteudo)
						.ThenInclude(c => c.Criador).ToList();

			foreach (var playlist in playlists)
			{
				playlist.Conteudos = playlist.Itens
					.Select(i => i.Conteudo).ToList();
			}

			return playlists;
		}

		public Playlist GetPlaylistByID(int id)
		{
			var itens = _context.ItemPlaylist.ToList();

			var playlist = _context.Playlist
				.Include(u => u.Usuario)
				.Include(i => i.Itens)
					.ThenInclude(i => i.Conteudo)
						.ThenInclude(c => c.Criador).FirstOrDefault(p => p.ID == id);

			if (playlist != null)
			{
				playlist.Conteudos = playlist.Itens
						.Select(i => i.Conteudo).ToList();
			}

			return playlist;
		}

		public void UpdatePlaylist(Playlist playlist)
		{
			var existingPlaylist = _context.Playlist
				  .Include(p => p.Itens)
				  .FirstOrDefault(p => p.ID == playlist.ID);

			if (existingPlaylist == null) return;

			existingPlaylist.Nome = playlist.Nome;

			existingPlaylist.Itens.Clear();

			foreach (var item in playlist.Itens)
			{
				existingPlaylist.Itens.Add(new ItemPlaylist
				{
					PlaylistID = existingPlaylist.ID,
					ConteudoID = item.ConteudoID
				});
			}

			_context.SaveChanges();
		}

		public void DeletePlaylist(int id)
		{
			var existingPlaylist = _context.Playlist
					  .Include(p => p.Itens)
					  .FirstOrDefault(p => p.ID == id);

			if (existingPlaylist == null) return;

			_context.ItemPlaylist.RemoveRange(existingPlaylist.Itens);

			_context.Playlist.Remove(existingPlaylist);

			_context.SaveChanges();
		}
	}
}
