using Microsoft.EntityFrameworkCore;
using super_heroi_api.Models;

namespace super_heroi_api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Herois> Herois { get; set; }
        public DbSet<Superpoderes> Superpoderes { get; set; }
        public DbSet<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabela de junção HeroisSuperpoderes
            modelBuilder.Entity<HeroisSuperpoderes>()
                .HasKey(hs => new { hs.HeroiId, hs.SuperpoderId }); // Chave composta

            modelBuilder.Entity<HeroisSuperpoderes>()
                .HasOne(hs => hs.Herois)
                .WithMany(h => h.HeroisSuperpoderes)
                .HasForeignKey(hs => hs.HeroiId);

            modelBuilder.Entity<HeroisSuperpoderes>()
                .HasOne(hs => hs.Superpoder)
                .WithMany()
                .HasForeignKey(hs => hs.SuperpoderId);

            // Configuração adicional de Superpoder
            modelBuilder.Entity<Superpoderes>()
                .Property(s => s.Superpoder)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Superpoderes>()
                .Property(s => s.Descricao)
                .HasMaxLength(250);
        }
    }
}
