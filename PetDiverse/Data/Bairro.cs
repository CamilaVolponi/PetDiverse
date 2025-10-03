using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class Bairro
    {
        public Bairro()
        {
            Doadores = new HashSet<PessoaDoadora>();
            Cidades = new Cidade();
        }
        public int Id { get; set; }
        public string NomeBairro { get; set; } = string.Empty;
        public virtual IEnumerable<PessoaDoadora>? Doadores { get; set; }
        public int IdCidade { get; set; }
        public virtual Cidade Cidades { get; set; }
    }
}
