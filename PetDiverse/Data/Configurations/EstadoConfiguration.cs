using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.NomeEstado).IsRequired();

            // FK
            builder.HasMany(x => x.Cidades).WithOne(x => x.Estados).HasForeignKey(x => x.IdEstado);
        }
    }
}
