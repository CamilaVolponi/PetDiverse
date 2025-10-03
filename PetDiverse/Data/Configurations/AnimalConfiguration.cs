using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

// anotações

namespace PetDiverse.Data.Configurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.Nome).IsRequired(); 

            builder.Property(x => x.Descricao).IsRequired();

            builder.Property(x => x.CaminhoFoto).IsRequired();

            // FK
            builder.HasOne(o => o.Doador).WithMany(u => u.Animais).HasForeignKey(o => o.IdDoador); // deixar assim

            builder.HasMany(x => x.RegistrosVacina).WithOne(x => x.Animal).HasForeignKey(x => x.IdAnimal); // apagar assim e colocar o de cima na outra classe

            builder.HasMany(x => x.RegistrosCirurgia).WithOne(x => x.Animal).HasForeignKey(x => x.IdAnimal);
            //HasMany = tem muitos //WithOne = com um //HasForeignKey = TemChaveEstrangeira
        }
    }
}
