using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class Bairro
    {
        public Bairro()
        {
            PessoasDoadoras = new HashSet<PessoaDoadora>();
        }
        public int Id { get; set; }
        [DisplayName("Bairro")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; } = string.Empty;
        public virtual ICollection<PessoaDoadora>? PessoasDoadoras { get; set; }
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int IdCidade { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
