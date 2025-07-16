using Microsoft.EntityFrameworkCore;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Data
{
    public class ParkManagerContext : DbContext
    {
        public ParkManagerContext(DbContextOptions<ParkManagerContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Mensalista> Mensalistas { get; set; }
        public DbSet<FaturamentoBasico> Faturamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefone).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Telefone).IsUnique();
            });

            // Configuração da entidade Veiculo
            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Placa).IsRequired().HasMaxLength(8);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Placa).IsUnique();

                // Relacionamento com Cliente
                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Veiculos)
                    .HasForeignKey(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração da entidade Mensalista
            modelBuilder.Entity<Mensalista>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Valor).HasColumnType("decimal(18,2)");

                // Relacionamentos
                entity.HasOne(e => e.Cliente)
                    .WithMany()
                    .HasForeignKey(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Veiculo)
                    .WithMany()
                    .HasForeignKey(e => e.VeiculoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração da entidade FaturamentoBasico
            modelBuilder.Entity<FaturamentoBasico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Valor).HasColumnType("decimal(18,2)");

                // Índice único para evitar duplicação de faturamento no mesmo mês
                entity.HasIndex(e => new { e.ClienteId, e.MesAno }).IsUnique();

                // Relacionamento com Cliente
                entity.HasOne(e => e.Cliente)
                    .WithMany()
                    .HasForeignKey(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}