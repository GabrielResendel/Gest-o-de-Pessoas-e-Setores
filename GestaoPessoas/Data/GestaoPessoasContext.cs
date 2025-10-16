using GestaoPessoas.Models;
using Microsoft.EntityFrameworkCore;
namespace GestaoPessoas.Data
{
    public class GestaoPessoasContext : DbContext
    {
        public GestaoPessoasContext(DbContextOptions<GestaoPessoasContext> options) : base(options)
        {
        }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Desempenho> Desempenhos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configuração para evitar muitos paths em cascata erro que obitive ao gerar update no bd

            modelBuilder.Entity<Desempenho>()
               .HasOne(d => d.Funcionario)
               .WithMany(f => f.Desempenhos)
               .HasForeignKey(d => d.FuncionarioId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Desempenho>()
                .HasOne(d => d.Meta)
                .WithMany(m => m.Desempenhos)
                .HasForeignKey(d => d.MetaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Setor)
                .WithMany(s => s.Funcionarios)
                .HasForeignKey(f => f.SetorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Meta>()
                .HasOne(m => m.Setor)
                .WithMany(s => s.Metas)
                .HasForeignKey(m => m.SetorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
