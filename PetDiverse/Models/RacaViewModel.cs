using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Models
{
    public class RacaViewModel
    {
        public int Id { get; set; }
        [DisplayName ("Raça")]
        public string Descricao { get; set; } = string.Empty;
        [DisplayName ("Tipo de animal")]
        public int IdTipoAnimal { get; set; }
    }
}
