using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PIMVIII.Models;

namespace PIMVIII.Services
{
	public class TokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(Usuario usuario)
		{
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
			var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, usuario.ID.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
				new Claim("nome", usuario.Nome)
			};

			var expiresHours = int.TryParse(_configuration["Jwt:ExpiresHours"], out var h) ? h : 4;

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddHours(expiresHours),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
