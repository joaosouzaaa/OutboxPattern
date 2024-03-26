using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tickets.Microservice.Entities;

namespace Tickets.Microservice.Data.EntitiesMapping;

public sealed class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
               .IsRequired(true)
               .HasColumnName("title")
               .HasColumnType("varchar(150)");

        builder.Property(t => t.Number)
               .IsRequired(true)
               .HasColumnName("number")
               .HasColumnType("int");

        builder.Property(t => t.Tag)
               .IsRequired(true)
               .HasColumnName("tag")
               .HasColumnType("varchar(150)");

        builder.Property(t => t.Description)
               .IsRequired(true)
               .HasColumnName("description")
               .HasColumnType("varchar(2000)");

        builder.Property(t => t.CreatedDate)
               .IsRequired(true)
               .HasColumnName("created_date")
               .HasColumnType("datetime2");

        builder.Property(t => t.FirstAppearance)
               .IsRequired(true)
               .HasColumnName("first_appearance")
               .HasColumnType("datetime2");
    }
}
