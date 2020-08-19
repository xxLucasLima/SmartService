using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region DbSets
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public DbSet<Empresa> Empresa { get; set; }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Lote> Lote { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<DefeitoProduto> DefeitoProduto { get; set; }
        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerfilUsuario>(entity =>
            {
                entity.HasKey(e => e.Id_PerfilUsuario)
                    .HasName("PK_perfilUsuario");

            });
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.Id_Empresa)
                    .HasName("PK_empresa");

            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id_Cliente)
                    .HasName("PK_cliente");

            });
            modelBuilder.Entity<Lote>(entity =>
            {
                entity.HasKey(e => e.Id_Lote)
                    .HasName("PK_lote");

            });
            modelBuilder.Entity<DefeitoProduto>(entity =>
            {
                entity.HasKey(e => e.Id_DefeitoProduto)
                    .HasName("PK_defeitoproduto");

            });
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id_Produto)
                    .HasName("PK_produto");

                entity.HasOne(d => d.Lote)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.Id_Lote)
                    .HasConstraintName("FK_produto_lote");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.Id_Cliente)
                    .HasConstraintName("FK_produto_cliente");

                entity.HasOne(d => d.DefeitoProduto)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.Id_DefeitoProduto)
                    .HasConstraintName("FK_produto_defeitoproduto");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id_Usuario)
                    .HasName("PK_usuario");

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Id_Empresa)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_usuario_empresa");

                entity.HasOne(d => d.PerfilUsuario)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Id_PerfilUsuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_usuario_perfilUsuario");
            });
        }

        #endregion
    }
}