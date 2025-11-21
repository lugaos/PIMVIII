using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public interface ICriadorRepository
    {
        List<Criador> GetAllCriadores();
        Criador GetCriadorByID(int id);
        void AddCriador(Criador criador);
        void UpdateCriador(Criador criador);
        void DeleteCriador(int id);
    }
}
