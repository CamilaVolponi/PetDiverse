using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class TipoCirurgia
    {
        public TipoCirurgia()
        {
            TipoAnimal = new TipoAnimal();
            RegistrosCirurgia = new HashSet<RegistroCirurgia>(); //definindo navegação, TipoCirurgia -> RegistroCirurgia
        }
        public int Id { get; set; }
        public string DescricaoCirurgia { get; set; } = string.Empty;
        public int IdTipoAnimal { get; set; } // campo FK - Tipo de animal
        public virtual TipoAnimal TipoAnimal { get; set; } // 1 TipoCirurgia -> 1 TipoAnimal
        public virtual IEnumerable<RegistroCirurgia> RegistrosCirurgia { get; set; } 
    }
}