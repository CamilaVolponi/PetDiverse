using PetDiverse.Data;
using PetDiverse.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Models
{
    public class PessoaDoadoraCadastroViewModel
    {
        public PessoaDoadoraCadastroViewModel()
        {
            TipoFormaContato = new List<TipoFormaContato>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo Obrigatório")] //colocar em todos, menos em ID
        public string Nome { get; set; } 
        public string Telefone { get; set; } = string.Empty;
        [DisplayName("Estado")]
        public int IdEstado { get; set; }
        [DisplayName("Cidade")]
        public int IdCidade { get; set; }
        [DisplayName("Bairro")]
        public int IdBairro { get; set; }
        public List<TipoFormaContato> TipoFormaContato { get; set; }
        public string CPF { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string CNPJ { get; set; } = string.Empty;
        public string Site { get; set; } 
        public string RedeSocial { get; set; } 
        public TipoPessoaCadastro TipoPessoaCadastro { get; set; }  
    }

    public enum TipoPessoaCadastro
    {
        Fisica = 1,
        Juridica = 2
    }
}
