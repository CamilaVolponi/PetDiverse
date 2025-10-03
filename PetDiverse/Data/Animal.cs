using PetDiverse.Data.Enuns;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class Animal
    {
        public Animal()
        {
            TipoAnimal = new TipoAnimal();
            Doador = new PessoaDoadora();
            RegistrosVacina = new HashSet<RegistroVacina>();
            RegistrosCirurgia = new HashSet<RegistroCirurgia>();
        }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // string.Empty = vai ter um valor, mais vazio = " "
        public string Descricao { get; set; } = string.Empty;
        public int Idade { get; set; }
        public bool Adotado { get; set; }
        public string? CaminhoFoto { get; set; } // "?" é pq pode ser nulo
        public PorteAnimal? Porte { get; set; }
        public int IdTipoAnimal { get; set; } // campo FK - TipoAnimal
        public virtual TipoAnimal TipoAnimal { get; set; } // 1 Animal -> 1 TipoAnimal, virtual = faz o join implicitamente
        public virtual IEnumerable<RegistroVacina>? RegistrosVacina { get; set; } // 1 Animal -> N Registros de vacina
        public virtual IEnumerable<RegistroCirurgia>? RegistrosCirurgia { get; set; } // 1 Animal -> N Registros de cirurgia
        public int IdDoador { get; set; } // campo FK - TipoAnimal
        public virtual PessoaDoadora Doador { get; set; } // N Animal -> 1 Doador
    }
}
