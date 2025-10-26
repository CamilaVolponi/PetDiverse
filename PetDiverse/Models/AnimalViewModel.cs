using Microsoft.AspNetCore.Mvc;
using PetDiverse.Data;
using PetDiverse.Data.Enuns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetDiverse.Models
{
    public class AnimalViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; } = string.Empty; // string.Empty = vai ter um valor, mais vazio = " "
        [DisplayName("Conte um pouco sobre o animal")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Descricao { get; set; } = string.Empty;
        //[MinLength(0, ErrorMessage = "Idade deve ser maior que 0")]
        [DisplayName("Idade")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Idade { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        public ContagemIdade? ContagemIdade { get; set; }
        public bool Adotado { get; set; }
        [NotMapped]
        public IFormFile? Foto { get; set; } // "?" é pq pode ser nulo
        public string? CaminhoFoto { get; set; }
        [DisplayName("Sexo biológico")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public SexoBiologico? SexoBiologico { get; set; }
        [DisplayName("Porte")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public PorteAnimal? Porte { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? IdTipoAnimal { get; set; } // campo FK - TipoAnimal
        [DisplayName("Raça")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? IdRaca { get; set; }
    }
}
