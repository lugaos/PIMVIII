using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public interface IConteudoRepository
    {
        List<Conteudo> GetAllConteudos();
        Conteudo GetConteudoByID(int id); 
        List<Conteudo> GetConteudosByIds(List<int> conteudo);
        void AddConteudo(Conteudo conteudo);
        void UpdateConteudo(Conteudo conteudo);
        void DeleteConteudo(int id);
    }
}
