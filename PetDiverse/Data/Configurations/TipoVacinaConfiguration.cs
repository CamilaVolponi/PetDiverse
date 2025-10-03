using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class TipoVacinaConfiguration : IEntityTypeConfiguration<TipoVacina>
    {
        public void Configure(EntityTypeBuilder<TipoVacina> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.DescricaoVacina).IsRequired();

            // FK
            builder.HasMany(x => x.RegistrosVacina).WithOne(x => x.TiposVacina).HasForeignKey(x => x.IdTipoVacina);
        }
    }
}
