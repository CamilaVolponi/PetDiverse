using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;

namespace PetDiverse.Models
{
    public class RegistroCirurgiaViewModel
    {
        public int Id { get; set; }
        public DateTime DataRegistro { get; set; }
        public int IdAnimal { get; set; }
        public int IdTipoCirurgia { get; set; }
    }
}
