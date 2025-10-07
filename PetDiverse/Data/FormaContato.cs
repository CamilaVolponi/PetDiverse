using PetDiverse.Data.Enums;
using PetDiverse.Data.Enuns;

namespace PetDiverse.Data
{
    public class FormaContato
    {
        public int Id { get; set; }
        public TipoFormaContato TipoFormaContato { get; set; }
        public int IdPesssoaDoadora { get; set; }
        public virtual PessoaDoadora PessoaDoadora { get; set; }
    }
}
