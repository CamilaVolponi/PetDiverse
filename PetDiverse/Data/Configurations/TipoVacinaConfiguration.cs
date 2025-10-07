using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class TipoVacinaConfiguration : IEntityTypeConfiguration<TipoVacina>
    {
        public void Configure(EntityTypeBuilder<TipoVacina> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.Descricao).IsRequired();

            // FK
            builder.HasOne(o => o.TipoAnimal).WithMany(u => u.TipoVacinas).HasForeignKey(o => o.IdTipoAnimal);
        }
    }
}
