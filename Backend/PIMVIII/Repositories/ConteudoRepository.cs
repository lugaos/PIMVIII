using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public class ConteudoRepository : IConteudoRepository
    {
        private readonly List<Conteudo> _conteudos = new List<Conteudo>();

        public Conteudo GetConteudoByID(int id) => _conteudos.FirstOrDefault(conteudo => conteudo.ID == id);

        public void AddConteudo(Conteudo conteudo) => _conteudos.Add(conteudo);

        public void UpdateConteudo(Conteudo conteudo)
        {
            var existing = GetConteudoByID(conteudo.ID);
            if (existing != null)
            {
                existing.Titulo = conteudo.Titulo;
                existing.Criador = conteudo.Criador;
                existing.Tipo = conteudo.Tipo;
            }
        }

        public void DeleteConteudo(int id)
        {
            var existing = GetConteudoByID(id);
            if (existing != null)
                _conteudos.Remove(existing);
        }

        public List<Conteudo> GetAllConteudos()
        {
            throw new NotImplementedException();
        }

        public List<Conteudo> GetConteudosByIds(List<int> conteudo)
        {
            throw new NotImplementedException();
        }
    }
}
