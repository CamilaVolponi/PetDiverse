using System.ComponentModel;

namespace PetDiverse.Data
{
    public class Raca
    {
        public Raca()
        {
            Animais = new HashSet<Animal>();            
        }
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public virtual ICollection<Animal> Animais { get; set; }
        public int IdTipoAnimal { get; set; }
        public virtual TipoAnimal TipoAnimal { get; set; }
    }
}
