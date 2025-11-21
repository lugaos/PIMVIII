using PIMVIII.Models;

namespace PIMVIII.DTOs
{
    public class UsuarioDto
    {

        /// <summary>Id do usuário.</summary>
        public int Id { get; set; }

        /// <summary>Nome do usuário.</summary>
        public string Nome { get; set; }

        /// <summary>Email do usuário.</summary>
        public string Email { get; set; }

        public UsuarioDto MapFrom(Usuario usuario)
        {
            return new UsuarioDto
            {
                Id = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email,
            };
        }
    }
}
