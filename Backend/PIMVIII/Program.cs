
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PIMVIII.Data;
using PIMVIII.Models;
using PIMVIII.Repositories;
using PIMVIII.Services;
using System.Reflection;
using System.Text;

namespace PIMVIII
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(options =>
			{
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				if (File.Exists(xmlPath))
				{
					options.IncludeXmlComments(xmlPath);
				}
			});

			builder.Services.AddAuthorization();
			builder.Services.AddScoped<TokenService>();

			builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
			builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
			builder.Services.AddScoped<TokenService>();

			var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "Bearer";
				options.DefaultChallengeScheme = "Bearer";
			})
			.AddJwtBearer("Bearer", options =>
			{
				options.RequireHttpsMetadata = true;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ClockSkew = TimeSpan.Zero
				};
			});

			builder.Services.AddAuthorization();

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				Seeder.SeedDatabase(context);
			}

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();

		}
	}
}
