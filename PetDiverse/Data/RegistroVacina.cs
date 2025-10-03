
namespace PetDiverse.Data
{
    public class RegistroVacina
    {
        public RegistroVacina()
        {
            TiposVacina = new TipoVacina();
        }
        public int Id { get; set; }
        public DateTime DataVacina {  get; set; }
        public int IdAnimal { get; set; } // campo FK
        public virtual Animal? Animal { get; set; }
        public int IdTipoVacina { get; set; } // campo FK - Tipo da Vicina
        public virtual TipoVacina TiposVacina { get; set; }
    }
}
