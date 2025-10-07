using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetDiverse.Data.Configurations
{
    public class FormaContatoConfiguration : IEntityTypeConfiguration<FormaContato>
    {
        public void Configure(EntityTypeBuilder<FormaContato> builder)
        {
            // FK
            builder.HasOne(o => o.PessoaDoadora).WithMany(u => u.FormasContato).HasForeignKey(o => o.IdPesssoaDoadora);
        }
    }
}
