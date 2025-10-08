using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetDiverse.Data;
using PetDiverse.Data.Configurations;
using System.Reflection.Emit;

namespace PetDiverse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<PessoaDoadora>().ToTable("PessoaDoadora");
            builder.Entity<PessoaFisica>().ToTable("PessoaFisica");
            builder.Entity<PessoaJuridica>().ToTable("PessoaJuridica");

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //builder.ApplyConfiguration(new AnimalConfiguration());
            //builder.ApplyConfiguration(new BairroConfiguration());
            //builder.ApplyConfiguration(new CidadeConfiguration());
            //builder.ApplyConfiguration(new EstadoConfiguration());
            //builder.ApplyConfiguration(new PessoaDoadoraConfiguration());
            //builder.ApplyConfiguration(new PessoaFisicaConfiguration());
            //builder.ApplyConfiguration(new PessoaJuridicaConfiguration());
            //builder.ApplyConfiguration(new RegistroCirurgiaConfiguration());
            //builder.ApplyConfiguration(new RegistroVacinaConfiguration());
            //builder.ApplyConfiguration(new TipoAnimalConfiguration());
            //builder.ApplyConfiguration(new TipoCirurgiaConfiguration());
            //builder.ApplyConfiguration(new TipoVacinaConfiguration());
        }
        public DbSet<PetDiverse.Data.TipoVacina> TipoVacina { get; set; } = default!;
        public DbSet<PetDiverse.Data.TipoCirurgia> TipoCirurgia { get; set; } = default!;
        public DbSet<PetDiverse.Data.TipoAnimal> TipoAnimal { get; set; } = default!;
        public DbSet<PetDiverse.Data.Estado> Estado { get; set; } = default!;
        public DbSet<PetDiverse.Data.Cidade> Cidade { get; set; } = default!;
        public DbSet<PetDiverse.Data.Bairro> Bairro { get; set; } = default!;
        public DbSet<PetDiverse.Data.PessoaDoadora> PessoaDoadora { get; set; } = default!;
        public DbSet<PetDiverse.Data.PessoaFisica> PessoaFisica { get; set; } = default!;
        public DbSet<PetDiverse.Data.PessoaJuridica> PessoaJuridica { get; set; } = default!;
        public DbSet<PetDiverse.Data.Animal> Animal { get; set; } = default!;
        public DbSet<PetDiverse.Data.RegistroCirurgia> RegistroCirurgia { get; set; } = default!;
        public DbSet<PetDiverse.Data.RegistroVacina> RegistroVacina { get; set; } = default!;
        public DbSet<PetDiverse.Data.Raca> Raca { get; set; } = default!;
    }
}
