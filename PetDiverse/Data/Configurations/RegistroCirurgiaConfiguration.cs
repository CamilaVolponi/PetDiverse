using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class RegistroCirurgiaConfiguration : IEntityTypeConfiguration<RegistroCirurgia>
    {
        public void Configure(EntityTypeBuilder<RegistroCirurgia> builder)
        {
            // FK
            builder.HasOne(o => o.Animal).WithMany(u => u.RegistrosCirurgia).HasForeignKey(o => o.IdAnimal);

            builder.HasOne(o => o.TipoCirurgia).WithMany(u => u.RegistrosCirurgia).HasForeignKey(o => o.IdTipoCirurgia);
        }
    }
}
