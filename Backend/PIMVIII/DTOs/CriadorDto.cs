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

        public CriadorDto MapFrom(Criador criador)
        {
           return new CriadorDto
            {
                Id = criador.ID,
                Nome = criador.Nome
            };
        }
    }
}