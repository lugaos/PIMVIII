using PIMVIII.Data;
using PIMVIII.Models;

namespace PIMVIII.Repositories
{
	public class UsuarioRepository : IUsuarioRepository
	{
		private readonly AppDbContext _context;

		public UsuarioRepository(AppDbContext context)
		{
			_context = context;
		}

		public Usuario? GetUsuarioByEmail(string email)
		{
			return _context.Usuario.FirstOrDefault(x => x.Email == email);
		}

		public Usuario? GetUsuarioById(int id)
		{
			return _context.Usuario.FirstOrDefault(x => x.ID == id);
		}

		public void AddUsuario(Usuario usuario)
		{
			_context.Usuario.Add(usuario);
			_context.SaveChanges();
		}

		public void UpdateUsuario(Usuario usuario)
		{
			var existingUsuario = GetUsuarioById(usuario.ID);
			if (existingUsuario == null) return;

			existingUsuario.Nome = usuario.Nome;
			existingUsuario.SenhaCriptografada = usuario.SenhaCriptografada;

			_context.SaveChanges();
		}
	}
}
