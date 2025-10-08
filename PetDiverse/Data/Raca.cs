using System.ComponentModel;

namespace PetDiverse.Data
{
    public class Raca
    {
        public Raca()
        {
            Animais = new HashSet<Animal>();
            TipoAnimal = new TipoAnimal();
        }
        public int Id { get; set; }
        [DisplayName("Raça")]
        public string Descricao { get; set; } = string.Empty;
        public virtual IEnumerable<Animal> Animais { get; set; }
        public int IdTipoAnimal { get; set; }
        [DisplayName("Tipo de animal")]
        public virtual TipoAnimal TipoAnimal { get; set; }
    }
}
