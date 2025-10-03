using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDiverse.Data;

namespace PetDiverse.Data.Configurations
{ 
    public class RegistroVacinaConfiguration : IEntityTypeConfiguration<RegistroVacina>
    {
        public void Configure(EntityTypeBuilder<RegistroVacina> builder)
        {
            
        }
    }
}
