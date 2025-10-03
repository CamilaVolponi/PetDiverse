using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class PessoaDoadora 
    {
        public PessoaDoadora()
        {
            Animais = new HashSet<Animal>();
            Bairro = new Bairro();
            Usuario = new Usuario();
        }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public virtual IEnumerable<Animal>? Animais { get; set; } // 1 Doador -> N Animal
        public int IdBairro { get; set; } 
        public virtual Bairro Bairro { get; set; }
        public string IdUsuario { get; set; } = string.Empty;
        public virtual Usuario Usuario { get; set; } 
    }
}
