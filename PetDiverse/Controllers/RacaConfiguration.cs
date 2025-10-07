using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDiverse.Data;

namespace PetDiverse.Controllers
{
    public class RacaConfiguration : IEntityTypeConfiguration<Raca>
    {
        public void Configure(EntityTypeBuilder<Raca> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.Descricao).IsRequired();

            //FK
            builder.HasOne(o => o.TipoAnimal).WithMany(u => u.Racas).HasForeignKey(o => o.IdTipoAnimal);
        }
    }
}
