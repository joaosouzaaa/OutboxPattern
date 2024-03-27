using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Support.Microservice.Entities;

namespace Support.Microservice.Data.EntitiesMapping;

public sealed class SupportMapping : IEntityTypeConfiguration<SupportEngineer>
{
    public void Configure(EntityTypeBuilder<SupportEngineer> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
               .IsRequired(true)
               .HasColumnName("name")
               .HasColumnType("varchar(200)");

        builder.Property(s => s.Email)
               .IsRequired(true)
               .HasColumnName("email")
               .HasColumnType("varchar(200)");

        builder.Property(s => s.IsEnabled)
               .IsRequired(true)
               .HasColumnName("is_enabled")
               .HasColumnType("bit");
    }
}
