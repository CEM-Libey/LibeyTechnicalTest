using LibeyTechnicalTestDomain.EFCore.Configuration;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain.Configuration;
using Microsoft.EntityFrameworkCore;
namespace LibeyTechnicalTestDomain.EFCore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<LibeyUser> LibeyUsers { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Ubigeo> Ubigeo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LibeyUserConfiguration());
        }
    }
}