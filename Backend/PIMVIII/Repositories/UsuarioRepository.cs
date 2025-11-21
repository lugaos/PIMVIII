using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();

        public List<Usuario> GetAllUsuarios() => _usuarios;

        public Usuario GetUsuarioByID(int id) => _usuarios.Find(usuario => usuario.ID == id);

        public void AddUsuario(Usuario usuario) => _usuarios.Add(usuario);

        public void UpdateUsuario(Usuario usuario)
        {
            var existing = GetUsuarioByID(usuario.ID);

            if (existing != null)
            {
                existing.Nome = usuario.Nome;
                existing.Email = usuario.Email;
                existing.Playlists = usuario.Playlists;
            }
        }

        public void DeleteUsuario(int id)
        {
            var existing = GetUsuarioByID(id);
            if (existing != null)
                _usuarios.Remove(existing);
        }
    }
}
