using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class PessoaJuridica : PessoaDoadora // Declaração da herança
    {
        public string CNPJ { get; set; } = string.Empty;
        public string RedeSocial { get; set; } = string.Empty;
        public string Site { get; set; } = string.Empty;
    }
}
