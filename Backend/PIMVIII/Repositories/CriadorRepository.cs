using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public class CriadorRepository : ICriadorRepository
    {
        private readonly List<Criador> _criadores = new List<Criador>();

        public List<Criador> GetAllCriadores() => _criadores;

        public Criador GetCriadorByID(int id) => _criadores.FirstOrDefault(criador => criador.ID == id);

        public void AddCriador(Criador criador) => _criadores.Add(criador);

        public void UpdateCriador(Criador criador)
        {
            var existing = GetCriadorByID(criador.ID);
            if (existing != null)
            {
                existing.Nome = criador.Nome;
                existing.Conteudos = criador.Conteudos;
            }
        }

        public void DeleteCriador(int id)
        {
            var existing = GetCriadorByID(id);
            if (existing != null)
                _criadores.Remove(existing);
        }
    }
}
