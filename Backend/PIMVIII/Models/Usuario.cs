using System.ComponentModel.DataAnnotations.Schema;

namespace PIMVIII.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;

        [NotMapped]
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
