using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public interface IPlaylistRepository
    {
        void AddPlaylist(Playlist playlist);
        List<Playlist> GetAllPlaylists();
        Playlist GetPlaylistByID(int id);
        void UpdatePlaylist(Playlist playlist);
        void DeletePlaylist(int id);
    }
}
