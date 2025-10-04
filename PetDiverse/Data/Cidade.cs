using System.ComponentModel.DataAnnotations;

namespace PetDiverse.Data
{
    public class Cidade
    {
        public Cidade()
        {
            Bairros = new HashSet<Bairro>();
            Estado = new Estado();
        }
        public int Id { get; set; }
        public string NomeCidade { get; set; } = string.Empty;
        public virtual IEnumerable<Bairro> Bairros { get; set; }
        public int IdEstado { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
