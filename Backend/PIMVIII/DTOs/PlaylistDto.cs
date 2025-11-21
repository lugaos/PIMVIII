using PIMVIII.Models;

namespace PIMVIII.DTOs
{
    /// <summary>
    /// Modelo da playlist.
    /// </summary>
    public class PlaylistDto
    {
        /// <summary>Identificador da playlist.</summary>
        public int Id { get; set; }
        /// <summary>Nome da playlist.</summary>
        public string Nome { get; set; }
        /// <summary>Dto do Usuario dono da playlist.</summary>
        public UsuarioDto Usuario { get; set; }
        /// <summary>Lista de Conteudos da Playlist.</summary>
        public List<ConteudoDto> Conteudos { get; set; }

        public static PlaylistDto MapFrom(Playlist playlist)
        {
            return new PlaylistDto
            {
                Id = playlist.ID,
                Nome = playlist.Nome,
                Usuario = new UsuarioDto().MapFrom(playlist.Usuario),
                Conteudos = playlist.Conteudos?.Select(conteudo => new ConteudoDto().MapFrom(conteudo)).ToList() ?? new List<ConteudoDto>()
            };
        }
    }
}