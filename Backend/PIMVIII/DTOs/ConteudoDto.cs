using PIMVIII.Models;

namespace PIMVIII.DTOs
{
    /// <summary>
    /// Modelo do conteúdo.
    /// </summary>
    public class ConteudoDto
    {
        /// <summary>Id do conteúdo.</summary>
        public int Id { get; set; }
        /// <summary>Título do conteúdo.</summary>
        public string Titulo { get; set; } = null!;
        /// <summary>Tipo do conteúdo.</summary>
        public string Tipo { get; set; } = null!;
        /// <summary>Criador do conteúdo.</summary>
        public CriadorDto Criador { get; set; } = null!;

        public ConteudoDto MapFrom(Conteudo conteudo)
        {
            return new ConteudoDto
            {
                Id = conteudo.ID,
                Titulo = conteudo.Titulo,
                Tipo = conteudo.Tipo,
                Criador = new CriadorDto().MapFrom(conteudo.Criador)
            };
        }
    }
}
