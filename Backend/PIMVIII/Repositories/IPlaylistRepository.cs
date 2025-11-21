using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public interface IPlaylistRepository
    {
        List<Playlist> GetAllPlaylists();
        Playlist GetPlaylistByID(int id);
        void AddPlaylist(Playlist playlist);
        void UpdatePlaylist(Playlist playlist);
        void DeletePlaylist(int id);
    }
}
