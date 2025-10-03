using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.NomeCidade).IsRequired();

            // FK
            builder.HasMany(x => x.Bairros).WithOne(x => x.Cidades).HasForeignKey(x => x.IdCidade);
        }
    }
}
