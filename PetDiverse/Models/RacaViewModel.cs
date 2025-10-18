using PetDiverse.Data;
using System.ComponentModel;

namespace PetDiverse.Models
{
    public class RacaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Raça")]
        public string Descricao { get; set; } = string.Empty;
        [DisplayName("Tipo de animal")]
        public int IdTipoAnimal { get; set; }
    }
}
