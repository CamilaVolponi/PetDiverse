using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDiverse.Data;

namespace PetDiverse.Data.Configurations
{ 
    public class RegistroVacinaConfiguration : IEntityTypeConfiguration<RegistroVacina>
    {
        public void Configure(EntityTypeBuilder<RegistroVacina> builder)
        {
            // FK
            builder.HasOne(o => o.Animal).WithMany(u => u.RegistrosVacina).HasForeignKey(o => o.IdAnimal);

            builder.HasOne(o => o.TipoVacina).WithMany(u => u.RegistrosVacina).HasForeignKey(o => o.IdTipoVacina);
        }
    }
}
