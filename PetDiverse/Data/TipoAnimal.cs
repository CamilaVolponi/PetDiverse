using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class TipoAnimal
    {
        public TipoAnimal()
        {
            Animais = new HashSet<Animal>(); //definindo navegação, TipoAnimal -> Animal
            TiposVacina = new HashSet<TipoVacina>(); //definindo navegação, TipoAnimal -> TipoVacina
            TiposCirurgia = new HashSet<TipoCirurgia>(); //definindo navegação, TipoAnimal -> TipoCirurgia
        }
        public int Id { get; set; }
        public string NomeTipoAnimal { get; set; } = string.Empty;
        public virtual IEnumerable<Animal> Animais { get; set; } // 1 TipoAnimal -> N Animais
        public virtual IEnumerable<TipoVacina> TiposVacina { get; set; } // 1 TipoAnimal -> N Tipos de vacina
        public virtual IEnumerable<TipoCirurgia> TiposCirurgia { get; set; } // 1 TipoAnimal -> N Tipos de cirurgia
    }
}
