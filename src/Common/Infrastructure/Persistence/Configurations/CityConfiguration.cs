namespace Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain.Entities;

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
