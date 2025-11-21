namespace PIMVIII.DTOs
{
    /// <summary>
    /// Modelo para atualizar uma playlist.
    /// </summary>
    public class PlaylistUpdateDto
    {
        /// <summary>Nome da playlist.</summary>
        public string Nome { get; set; }

        /// <summary>Identificador dos conteudos da playlist.</summary>
        public List<int> IdConteudos { get; set; }
    }
}
