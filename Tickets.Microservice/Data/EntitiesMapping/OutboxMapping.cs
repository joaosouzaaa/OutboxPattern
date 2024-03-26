using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tickets.Microservice.Entities;

namespace Tickets.Microservice.Data.EntitiesMapping;

public sealed class OutboxMapping : IEntityTypeConfiguration<Outbox>
{
    public void Configure(EntityTypeBuilder<Outbox> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedDate)
               .IsRequired(true)
               .HasColumnName("created_date")
               .HasColumnType("datetime2");

        builder.Property(o => o.Content)
               .IsRequired(true)
               .HasColumnName("content")
               .HasColumnType("varchar(MAX)");

        builder.Property(o => o.Type)
               .IsRequired(true)
               .HasColumnName("type")
               .HasColumnType("varchar(100)");

        builder.Property(o => o.ProcessedDate)
               .IsRequired(false)
               .HasColumnName("processed_date")
               .HasColumnType("datetime2");
    }
}
