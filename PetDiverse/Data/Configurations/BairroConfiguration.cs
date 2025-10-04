using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class BairroConfiguration : IEntityTypeConfiguration<Bairro>
    {
        public void Configure(EntityTypeBuilder<Bairro> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.NomeBairro).IsRequired();

            // FK
            builder.HasOne(o => o.Cidade).WithMany(u => u.Bairros).HasForeignKey(o => o.IdCidade);
        }
    }
}
