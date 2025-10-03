using PetDiverse.Data;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class PessoaFisica : PessoaDoadora // Declaração da herança
    {
        public string CPF { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
    }
}
