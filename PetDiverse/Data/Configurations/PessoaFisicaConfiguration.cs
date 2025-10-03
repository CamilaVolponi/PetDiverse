using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class PessoaFisicaConfiguration : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            // Required = string é obrigatórias
            builder.Property(x => x.CPF).IsRequired();

            builder.Property(x => x.Sobrenome).IsRequired();
        }
    }
}
