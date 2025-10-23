using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Models
{
    public class TipoVacinaViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Vacina")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Descricao { get; set; } = string.Empty;
        [DisplayName ("Tipo de animal")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? IdTipoAnimal { get; set; } // campo FK

    }
}
