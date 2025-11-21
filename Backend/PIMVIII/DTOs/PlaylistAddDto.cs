namespace PIMVIII.DTOs
{
    /// <summary>
    /// Modelo para criar uma nova playlist
    /// </summary>
    public class PlaylistAddDto
    {
        /// <summary>Nome da playlist.</summary>
        public string Nome { get; set; }

        /// <summary>Id do usuário criador da playlist.</summary>
        public int IdUsuario { get; set; }

        /// <summary>Lista de ids dos conteúdos da playlist.</summary>
        public List<int> IdConteudos { get; set; }
    }
}
