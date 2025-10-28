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
        [Required(ErrorMessage ="Campo Obrigatório")] 
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Telefone { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Estado")]
        public int IdEstado { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Cidade")]
        public int IdCidade { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Bairro")]
        public int IdBairro { get; set; }
        [MinLength(1, ErrorMessage = "Selecione pelo menos uma forma de contato")]
        [DisplayName("Tipo forma de contato")]
        public List<TipoFormaContato>? TipoFormaContato { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CPF { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Data de nascimento")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CNPJ { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Site { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Rede social")]
        public string RedeSocial { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DisplayName("Tipo de pessoa")]
        public TipoPessoaCadastro? TipoPessoaCadastro { get; set; }  
    }

    public enum TipoPessoaCadastro
    {
        Fisica = 1,
        Juridica = 2
    }
}
