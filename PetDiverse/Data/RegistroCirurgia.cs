
namespace PetDiverse.Data
{
    public class RegistroCirurgia
    {
        public RegistroCirurgia()
        {
            TipoCirurgia = new TipoCirurgia();
        }
        public int Id { get; set; }
        public DateTime DataCirurgia { get; set; }
        public int IdAnimal { get; set; } // campo FK - Animail
        public virtual Animal? Animal { get; set; }
        public int IdTipoCirurgia { get; set; } // campo FK - Tipo da Cirurgia
        public virtual TipoCirurgia TipoCirurgia { get; set; }
    }
}
