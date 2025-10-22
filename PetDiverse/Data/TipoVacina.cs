using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class TipoVacina
    {
        public TipoVacina()
        {
            RegistrosVacina = new HashSet<RegistroVacina>(); //definindo navegação, TipoVacina -> RegistroVacina
        }
        public int Id { get; set; }
        [Display(Name = "Vacina")]
        public string Descricao { get; set; } = string.Empty;
        public int IdTipoAnimal { get; set; } // campo FK
        [Display(Name = "Tipo de animal")]
        public virtual TipoAnimal TipoAnimal { get; set; } // 1 TipoVacina -> 1 TipoAnimal
        public virtual ICollection<RegistroVacina> RegistrosVacina { get; set; }
    }
}
