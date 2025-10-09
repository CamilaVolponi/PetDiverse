using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class TipoCirurgia
    {
        public TipoCirurgia()
        {
            RegistrosCirurgia = new HashSet<RegistroCirurgia>(); //definindo navegação, TipoCirurgia -> RegistroCirurgia
        }
        public int Id { get; set; }
        [Display(Name = "Cirurgia")]
        public string Descricao { get; set; } = string.Empty;
        public int IdTipoAnimal { get; set; } // campo FK - Tipo de animal
        [Display(Name = "Tipo de animal")]
        public virtual TipoAnimal TipoAnimal { get; set; } // 1 TipoCirurgia -> 1 TipoAnimal
        public virtual ICollection<RegistroCirurgia> RegistrosCirurgia { get; set; } 
    }
}