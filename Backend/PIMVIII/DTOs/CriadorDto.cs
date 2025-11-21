using PIMVIII.Models;

namespace PIMVIII.DTOs
{
    /// <summary>
    /// Modelo de criador de conteúdo.
    /// </summary>
    public class CriadorDto
    {
        /// <summary>Id do criador.</summary>
        public int Id { get; set; }
        /// <summary>Nome do criador.</summary>
        public string Nome { get; set; }
        /// <summary>Lista de Conteudos do criador.</summary>
        public List<ConteudoDto> Conteudos { get; set; }

        public CriadorDto MapFrom(Criador criador)
        {
           return new CriadorDto
            {
                Id = criador.ID,
                Nome = criador.Nome,
                Conteudos = criador.Conteudos?.Select(conteudo => new ConteudoDto().MapFrom(conteudo)).ToList() ?? new List<ConteudoDto>()
            };
        }
    }
}