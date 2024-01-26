using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using NutriSoftwareV1.Models;


namespace NutriSoftwareV1.Data
{
    public class NutriDbContext : DbContext
    {

        public DbSet<Alimento> Alimento { get; set; }
        public DbSet<Anminese> Anminese { get; set; }
        public DbSet<AtividadesFisicas> AtividadeFisica { get; set; }
        public DbSet<AvaliacaoFisica> AvaliacoesFisicas { get; set; }
        public DbSet<AvalicaoBioImpedancia> AvalicaoBioImpedancia { get; set; }
        public DbSet<ConsumoAlimentar> ConsumoAlimentar { get; set; }
        public DbSet<Doenca> Doenca { get; set; }
        public DbSet<LocalAlmoco> LocalAlmoco { get; set; }
        public DbSet<LocalAtendimento> LocalAtendimento { get; set; }
        public DbSet<Observacao> AnotacosPaciente { get; set; }
        public DbSet<Paciente> pacientes{get ; set;}
        public DbSet<Pagamento> Pagamento { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost; Database=softnutricaov1; Convert Zero Datetime=True; Uid=root; Pwd=123456;",
                    options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Anotacoes)
            .WithOne(f => f.Paciente)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutriDbContext).Assembly);
        }
    }
}