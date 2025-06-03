using LibeyTechnicalTestDomain.EFCore.Configuration;
using LibeyTechnicalTestDomain.EFCore.Entities;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
namespace LibeyTechnicalTestDomain.EFCore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<LibeyUser> LibeyUsers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Ubigeo> Ubigeos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LibeyUserConfiguration());
            
            // Configuración de las entidades de catálogos
            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");
                entity.HasKey(e => e.RegionCode);
                entity.Property(e => e.RegionCode).HasMaxLength(2).IsRequired();
                entity.Property(e => e.RegionDescription).HasMaxLength(200);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");
                entity.HasKey(e => e.ProvinceCode);
                entity.Property(e => e.ProvinceCode).HasMaxLength(4).IsRequired();
                entity.Property(e => e.RegionCode).HasMaxLength(2).IsRequired();
                entity.Property(e => e.ProvinceDescription).HasMaxLength(200);
                
                entity.HasOne(d => d.Region)
                    .WithMany()
                    .HasForeignKey(d => d.RegionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Ubigeo>(entity =>
            {
                entity.ToTable("Ubigeo");
                entity.HasKey(e => e.UbigeoCode);
                entity.Property(e => e.UbigeoCode).HasMaxLength(6).IsRequired();
                entity.Property(e => e.ProvinceCode).HasMaxLength(4).IsRequired();
                entity.Property(e => e.RegionCode).HasMaxLength(2).IsRequired();
                
                entity.HasOne(d => d.Province)
                    .WithMany()
                    .HasForeignKey(d => d.ProvinceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    
                entity.HasOne(d => d.Region)
                    .WithMany()
                    .HasForeignKey(d => d.RegionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}