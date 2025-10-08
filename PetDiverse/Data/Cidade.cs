using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class Cidade
    {
        public Cidade()
        {
            Bairros = new HashSet<Bairro>();
        }
        public int Id { get; set; }
        [DisplayName("Cidade")]
        public string Nome { get; set; } = string.Empty;
        public virtual IEnumerable<Bairro> Bairros { get; set; }
        [DisplayName("Estado")]
        public int IdEstado { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
