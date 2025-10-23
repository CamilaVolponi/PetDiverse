using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Models
{
    public class BairroViewModel
    {
        public int Id { get; set; }
        [DisplayName("Bairro")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; } = string.Empty;
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int IdCidade { get; set; }
    }
}
