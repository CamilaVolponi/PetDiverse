using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Models
{
    public class TipoCirurgiaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Cirurgia")]
        public string Descricao { get; set; } = string.Empty;
        [Display(Name = "Tipo de animal")]
        public int IdTipoAnimal { get; set; } // campo FK - Tipo de animal
    }
}
