using PetDiverse.Data;
using PetDiverse.Data.Enums;
using System.ComponentModel;

namespace PetDiverse.Models
{
    public class PessoaDoadoraCadastroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        [DisplayName("Estado")]
        public int IdEstado { get; set; }
        [DisplayName("Cidade")]
        public int IdCidade { get; set; }
        [DisplayName("Bairro")]
        public int IdBairro { get; set; }
        public List<TipoFormaContato> TipoFormaContato { get; set; }
        public string CPF { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Site { get; set; } = string.Empty;
        public TipoPessoaCadastro TipoPessoaCadastro { get; set; }  
    }

    public enum TipoPessoaCadastro
    {
        Fisica = 1,
        Juridica = 2
    }
}
