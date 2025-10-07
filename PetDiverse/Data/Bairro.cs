using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class Bairro
    {
        public Bairro()
        {
            PessoasDoadoras = new HashSet<PessoaDoadora>();
            Cidade = new Cidade();
        }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public virtual IEnumerable<PessoaDoadora>? PessoasDoadoras { get; set; }
        [DisplayName("Cidade")]
        public int IdCidade { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
