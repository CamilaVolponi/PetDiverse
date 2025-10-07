using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class Estado
    {
        public Estado()
        {
            Cidades = new HashSet<Cidade>();
        }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public virtual IEnumerable<Cidade> Cidades { get; set; }
    }
}
