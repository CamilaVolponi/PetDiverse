using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Models
{
    public class RegistroVacinaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Data de registro")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime? DataRegistro { get; set; }
        public int IdAnimal { get; set; }
        [DisplayName("Tipo vacina")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int IdTipoVacina { get; set; }
    }
}
