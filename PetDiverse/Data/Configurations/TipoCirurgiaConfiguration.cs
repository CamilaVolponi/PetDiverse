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
            builder.HasOne(o => o.TipoAnimal).WithMany(u => u.TipoCirurgias).HasForeignKey(o => o.IdTipoAnimal);
        }
    }
}
