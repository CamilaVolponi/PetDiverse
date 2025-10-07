using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.Nome).IsRequired();

            // FK
            builder.HasOne(o => o.Estado).WithMany(u => u.Cidades).HasForeignKey(o => o.IdEstado);
        }
    }
}
