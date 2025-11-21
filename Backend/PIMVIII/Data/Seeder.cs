using PIMVIII.Models;

namespace PIMVIII.Data
{
    public class Seeder
    {
        public static void SeedDatabase(AppDbContext context)
        {
            if (context.Criador.Any() || context.Conteudo.Any() || context.Usuario.Any())
                return;

            var criadores = new List<Criador>
            {
                new Criador { Nome = "Criador A" },
                new Criador { Nome = "Criador B" },
                new Criador { Nome = "Criador C" },
                new Criador { Nome = "Criador D" }
            };

            context.Criador.AddRange(criadores);
            context.SaveChanges();

            var conteudos = new List<Conteudo>();
            int conteudoCount = 1;

            foreach (var criador in criadores)
            {
                for (int i = 1; i <= 5; i++)
                {
                    conteudos.Add(new Conteudo
                    {
                        Titulo = $"Conteudo {conteudoCount} do {criador.Nome}",
                        Tipo = i % 2 == 0 ? "Audio" : "Video",
                        CriadorID = criador.ID
                    });
                    conteudoCount++;
                }
            }

            context.Conteudo.AddRange(conteudos);
            context.SaveChanges();

            var usuarios = new List<Usuario>
            {
                new Usuario { Nome = "Lucas", Email = "lucas@email.com" },
                new Usuario { Nome = "Ana", Email = "ana@email.com" },
                new Usuario { Nome = "Bruno", Email = "bruno@email.com" },
                new Usuario { Nome = "Maria", Email = "maria@email.com" }
            };

            context.Usuario.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}
