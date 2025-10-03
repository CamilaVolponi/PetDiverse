using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class PessoaJuridicaConfiguration : IEntityTypeConfiguration<PessoaJuridica>
    {
        public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.CNPJ).IsRequired();

            builder.Property(x => x.RedeSocial).IsRequired();

            builder.Property(x => x.Site).IsRequired();

        }
    }
}
