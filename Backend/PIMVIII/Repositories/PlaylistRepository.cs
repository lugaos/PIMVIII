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

        public void DeletePlaylist(int id)
        {
            var playlist = _context.Playlist.Find(id);

            if (playlist != null)
            {
                _context.Playlist.Remove(playlist);
                _context.SaveChanges();
            }
        }

        public List<Playlist> GetAllPlaylists()
        {
            var playlists = _context.Playlist.ToList();

            foreach (var playlist in playlists)
            {
                playlist.Conteudos = playlist.Itens.Select(i => i.Conteudo).ToList();
            }

            return playlists;
        }

        public Playlist GetPlaylistByID(int id)
        {
            var playlist = _context.Playlist.Find(id);

            playlist?.Conteudos = playlist.Itens.Select(i => i.Conteudo).ToList();

            return playlist ?? new Playlist();
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            var existingPlaylist = _context.Playlist
                  .Include(p => p.Itens)
                  .FirstOrDefault(p => p.ID == playlist.ID);

            if (existingPlaylist == null) return;

            existingPlaylist.Nome = playlist.Nome;
            existingPlaylist.UsuarioID = playlist.UsuarioID;

            var itensParaRemover = existingPlaylist.Itens
                .Where(ip => !playlist.Conteudos.Any(c => c.ID == ip.ConteudoID))
                .ToList();

            foreach (var ip in itensParaRemover)
                existingPlaylist.Itens.Remove(ip);

            var conteudosExistentesIds = existingPlaylist.Itens.Select(ip => ip.ConteudoID).ToList();

            var novosItens = playlist.Conteudos
                .Where(c => !conteudosExistentesIds.Contains(c.ID))
                .Select(c => new ItemPlaylist
                {
                    PlaylistID = existingPlaylist.ID,
                    ConteudoID = c.ID
                })
                .ToList();

            foreach (var ip in novosItens)
                existingPlaylist.Itens.Add(ip);

            _context.SaveChanges();
        }
    }
}
