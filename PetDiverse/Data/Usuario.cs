using Microsoft.AspNetCore.Identity;

namespace PetDiverse.Data
{
    public class Usuario : IdentityUser
    {
        public Usuario()
        {
            PessoaDoadora = new HashSet<PessoaDoadora>();
        }
        public virtual ICollection<PessoaDoadora> PessoaDoadora { get; set; }
    }
}
