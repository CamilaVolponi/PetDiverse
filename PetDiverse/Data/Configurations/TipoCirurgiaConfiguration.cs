using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class TipoCirurgiaConfiguration : IEntityTypeConfiguration<TipoCirurgia>
    {
        public void Configure(EntityTypeBuilder<TipoCirurgia> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.DescricaoCirurgia).IsRequired();

            // FK
            builder.HasMany(x => x.RegistrosCirurgia).WithOne(x => x.TiposCirurgia).HasForeignKey(x => x.IdTipoCirurgia);
        }
    }
}
