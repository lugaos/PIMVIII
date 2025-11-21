using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAllUsuarios();
        Usuario GetUsuarioByID(int id);
        void AddUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void DeleteUsuario(int id);
    }
}
