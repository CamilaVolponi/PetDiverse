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
        public string DescricaoRaca { get; set; } = string.Empty;
        public virtual IEnumerable<Animal> Animais { get; set; }
        public int IdTipoAnimal { get; set; }
        public virtual TipoAnimal TipoAnimal { get; set; }
    }
}
