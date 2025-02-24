using LibeyTechnicalTestDomain.EFCore.Configuration;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibeyTechnicalTestDomain.EFCore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        // DbSets para mapear todas las tablas de la base de datos (asegurando que los nombres coincidan con la base de datos)
        public DbSet<LibeyUser> LibeyUsers { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; } // Cambiado a DocumentType para coincidir con el nombre real de la tabla
        public DbSet<Ubigeo> Ubigeo { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Region> Region { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar configuración específica para LibeyUser
            modelBuilder.ApplyConfiguration(new LibeyUserConfiguration());

            // Configuración de DocumentType
            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType"); // Especifica el nombre exacto de la tabla
                entity.HasKey(e => e.DocumentTypeId);
                entity.Property(e => e.DocumentTypeId).IsRequired();
                entity.Property(e => e.DocumentTypeDescription).HasMaxLength(200);
            });

            // Configuración de Ubigeo
            modelBuilder.Entity<Ubigeo>(entity =>
            {
                entity.ToTable("Ubigeo");
                entity.HasKey(e => e.UbigeoCode);
                entity.Property(e => e.UbigeoCode).IsRequired().HasMaxLength(6);
                entity.Property(e => e.UbigeoDescription).HasMaxLength(200);

                entity.HasOne(e => e.Province)
                      .WithMany(p => p.Ubigeos)
                      .HasForeignKey(e => e.ProvinceCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Region)
                      .WithMany(r => r.Ubigeos)
                      .HasForeignKey(e => e.RegionCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Province
            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");
                entity.HasKey(e => e.ProvinceCode);
                entity.Property(e => e.ProvinceCode).IsRequired().HasMaxLength(4);
                entity.Property(e => e.ProvinceDescription).HasMaxLength(200);

                entity.HasOne(e => e.Region)
                      .WithMany(r => r.Provinces)
                      .HasForeignKey(e => e.RegionCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Region
            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");
                entity.HasKey(e => e.RegionCode);
                entity.Property(e => e.RegionCode).IsRequired().HasMaxLength(2);
                entity.Property(e => e.RegionDescription).HasMaxLength(200);
            });

            // Configuración de relaciones para LibeyUser
            modelBuilder.Entity<LibeyUser>(entity =>
            {
                entity.ToTable("LibeyUser");
                entity.HasKey(e => e.DocumentNumber);
                entity.Property(e => e.DocumentNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.FathersLastName).HasMaxLength(100);
                entity.Property(e => e.MothersLastName).HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(20);
                entity.Property(e => e.Password).HasMaxLength(20);
                entity.Property(e => e.UbigeoCode).HasMaxLength(6);

                entity.HasOne(e => e.DocumentType)
                      .WithMany(dt => dt.LibeyUsers)
                      .HasForeignKey(e => e.DocumentTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Ubigeo)
                      .WithMany(u => u.LibeyUsers)
                      .HasForeignKey(e => e.UbigeoCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}