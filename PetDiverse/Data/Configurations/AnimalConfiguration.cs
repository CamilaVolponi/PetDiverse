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
            builder.HasOne(o => o.PessoaDoadora).WithMany(u => u.Animais).HasForeignKey(o => o.IdPessoaDoadora);
            //HasOne = tem um //WithMany = para muitos //HasForeignKey = TemChaveEstrangeira
            
            builder.HasOne(o => o.TipoAnimal).WithMany(u => u.Animais).HasForeignKey(o => o.IdTipoAnimal);

            builder.HasOne(o => o.Raca).WithMany(u => u.Animais).HasForeignKey(o => o.IdRaca);
        }
    }
}
