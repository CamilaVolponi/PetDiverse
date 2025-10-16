using System.ComponentModel;
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
        [DisplayName("Estado")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Sigla { get; set; } = string.Empty;
        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}
