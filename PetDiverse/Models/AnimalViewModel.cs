using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;

namespace PetDiverse.Models
{
    public class AnimalViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // string.Empty = vai ter um valor, mais vazio = " "
        public string Descricao { get; set; } = string.Empty;
        //[MinLength(0, ErrorMessage = "Idade deve ser maior que 0")]
        public int Idade { get; set; }
        public ContagemIdade ContagemIdade { get; set; }
        public bool Adotado { get; set; }
        public IFormFile Foto { get; set; } // "?" é pq pode ser nulo
        public SexoBiologico SexoBiologico { get; set; }
        public PorteAnimal? Porte { get; set; }
        public int IdTipoAnimal { get; set; } // campo FK - TipoAnimal
        public int IdRaca { get; set; }
    }
}
