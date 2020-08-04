using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public DbSet<Empresa> Empresa { get; set; }

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
    }
}