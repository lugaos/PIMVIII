using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public interface IUsuarioRepository
    {
		Usuario? GetUsuarioByEmail(string email);
		Usuario? GetUsuarioById(int id);
        void AddUsuario(Usuario usuario);
		void UpdateUsuario(Usuario usuario);
	}
}
