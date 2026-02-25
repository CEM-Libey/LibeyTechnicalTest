using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LibeyTechnicalTestDomain.EFCore.Configuration
{
    internal class LibeyUserConfiguration : IEntityTypeConfiguration<LibeyUser>
    {
        public void Configure(EntityTypeBuilder<LibeyUser> builder)
        {
            builder.ToTable("LibeyUser").HasKey(x => x.DocumentNumber);

            // Configure column lengths to avoid truncation issues in the database
            builder.Property(x => x.DocumentNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.FathersLastName).HasMaxLength(100);
            builder.Property(x => x.MothersLastName).HasMaxLength(100);
            builder.Property(x => x.Address).HasMaxLength(250);
            builder.Property(x => x.UbigeoCode).HasMaxLength(10);
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.Password).HasMaxLength(200);
        }
    }
}