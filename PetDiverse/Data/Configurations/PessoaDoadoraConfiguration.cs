using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class PessoaDoadoraConfiguration : IEntityTypeConfiguration<PessoaDoadora>
    {
        public void Configure(EntityTypeBuilder<PessoaDoadora> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.Nome).IsRequired();

            builder.Property(x => x.Telefone).IsRequired();

            builder.Property(x => x.IdUsuario).IsRequired();

            // FK
            builder.HasOne(o => o.Usuario).WithMany(u => u.PessoaDoadora).HasForeignKey(o => o.IdUsuario);
        }
    }
}
