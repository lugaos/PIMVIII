using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIMVIII.DTOs;
using PIMVIII.Models;
using PIMVIII.Repositories;
using PIMVIII.Services;
using static PIMVIII.DTOs.UsuarioAddDto;

namespace PIMVIII.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUsuarioRepository _usuarios;
		private readonly IPasswordHasher<Usuario> _passwordHasher;
		private readonly TokenService _tokenService;

		public AuthController(IUsuarioRepository usuarios, IPasswordHasher<Usuario> passwordHasher, TokenService tokenService)
		{
			_usuarios = usuarios;
			_passwordHasher = passwordHasher;
			_tokenService = tokenService;
		}

		/// <summary>
		/// Cadastra um novo usuário no sistema
		/// </summary>
		/// <returns>Cadastrar Usuario</returns>
		[HttpPost]
		public ActionResult Add(UsuarioAddDto dto)
		{
			var existing = _usuarios.GetUsuarioByEmail(dto.Email);
			if (existing != null)
				return BadRequest(new { message = "Email já cadastrado." });

			var usuario = new Usuario
			{
				Nome = dto.Nome,
				Email = dto.Email
			};

			usuario.SenhaCriptografada = _passwordHasher.HashPassword(usuario, dto.Senha);

			_usuarios.AddUsuario(usuario);

			return CreatedAtAction(null, new { id = usuario.ID }, new { id = usuario.ID });
		}

		/// <summary>
		/// Atualiza um usuário no sistema
		/// </summary>
		/// <returns>Atualizar Usuario</returns>
		[HttpPut("atualizarSenha/{id}")]
		public ActionResult Update(int id, UsuarioUpdateDto dto)
		{
			var existing = _usuarios.GetUsuarioById(id);
			if (existing == null)
				return BadRequest(new { message = "Usuário não localizado" });

			var usuario = new Usuario
			{
				ID = id,
				Nome = dto.Nome
			};

			usuario.SenhaCriptografada = _passwordHasher.HashPassword(usuario, dto.Senha);

			_usuarios.UpdateUsuario(usuario);

			return Ok();
		}

		/// <summary>
		/// Efetua login do usuário e obtem o token de acesso
		/// </summary>
		/// <returns>Token</returns>
		[HttpPost("login")]
		public ActionResult Login(LoginDto dto)
		{
			var usuario = _usuarios.GetUsuarioByEmail(dto.Email);
			if (usuario == null)
				return Unauthorized(new { message = "Credenciais inválidas." });

			var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaCriptografada, dto.Senha);

			if (result == PasswordVerificationResult.Failed)
				return Unauthorized(new { message = "Credenciais inválidas." });

			var token = _tokenService.GenerateToken(usuario);
			return Ok(new { token });
		}
	}
}
