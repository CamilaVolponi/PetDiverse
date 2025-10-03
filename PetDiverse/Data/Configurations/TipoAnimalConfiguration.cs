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


            // FK

            builder.HasMany(x => x.Animais).WithOne(x => x.TipoAnimal).HasForeignKey(x => x.IdTipoAnimal);

            builder.HasMany(x => x.TiposVacina).WithOne(x => x.TipoAnimal).HasForeignKey(x => x.IdTipoAnimal);

            builder.HasMany(x => x.TiposCirurgia).WithOne(x => x.TipoAnimal).HasForeignKey(x => x.IdTipoAnimal);

        }
    }
}
