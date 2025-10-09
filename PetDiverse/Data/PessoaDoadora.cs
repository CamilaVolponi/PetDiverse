using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class PessoaDoadora 
    {
        public PessoaDoadora()
        {
            Animais = new HashSet<Animal>();
            FormasContato = new HashSet<FormaContato>();
        }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public virtual ICollection<Animal>? Animais { get; set; } // 1 Doador -> N Animal
        [DisplayName("Bairro")]
        public int IdBairro { get; set; } 
        public virtual Bairro Bairro { get; set; }
        public string IdUsuario { get; set; } = string.Empty;
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<FormaContato> FormasContato { get; set; } 
    }
}
