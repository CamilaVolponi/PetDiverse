using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class TipoAnimal
    {
        public TipoAnimal()
        {
            Animais = new HashSet<Animal>(); //definindo navegação, TipoAnimal -> Animal
            TipoVacinas = new HashSet<TipoVacina>(); //definindo navegação, TipoAnimal -> TipoVacina
            TipoCirurgias = new HashSet<TipoCirurgia>(); //definindo navegação, TipoAnimal -> TipoCirurgia
            Racas = new HashSet<Raca>();
        }
        public int Id { get; set; }
        public string DescricaoTipoAnimal { get; set; } = string.Empty;
        public virtual IEnumerable<Animal> Animais { get; set; } // 1 TipoAnimal -> N Animais
        public virtual IEnumerable<TipoVacina> TipoVacinas { get; set; } // 1 TipoAnimal -> N Tipos de vacina
        public virtual IEnumerable<TipoCirurgia> TipoCirurgias { get; set; } // 1 TipoAnimal -> N Tipos de cirurgia
        public virtual IEnumerable<Raca> Racas { get; set; }
    }
}
