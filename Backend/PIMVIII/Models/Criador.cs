using System.ComponentModel.DataAnnotations.Schema;

namespace PIMVIII.Models
{
    public class Criador
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public List<Conteudo> Conteudos { get; set; } = new List<Conteudo>();
    }
}
