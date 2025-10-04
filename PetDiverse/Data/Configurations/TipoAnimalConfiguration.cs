using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class TipoAnimalConfiguration : IEntityTypeConfiguration<TipoAnimal>
    {
        public void Configure(EntityTypeBuilder<TipoAnimal> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.NomeTipoAnimal).IsRequired();
        }
    }
}
