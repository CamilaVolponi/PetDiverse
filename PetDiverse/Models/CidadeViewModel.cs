using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Models
{
    public class CidadeViewModel
    {
        public int Id { get; set; }
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; } = string.Empty;
        [DisplayName("Estado")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int IdEstado { get; set; }
    }
}
