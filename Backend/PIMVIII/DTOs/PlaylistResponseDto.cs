namespace PIMVIII.DTOs
{
    /// <summary>
    /// Modelo de retorno de uma playlist.
    /// </summary>
    public class PlaylistResponseDto
    {
        /// <summary>Id da playlist.</summary>
        public int Id { get; set; }

        /// <summary>Nome da playlist.</summary>
        public string Nome { get; set; }

        /// <summary>Informações do usuário dono da playlist.</summary>
        public UsuarioDto Usuario { get; set; }

        /// <summary>Lista de conteúdos da playlist.</summary>
        public List<ConteudoDto> Conteudos { get; set; }
    }
}
