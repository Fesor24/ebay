using Ebay.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebay.Infrastructure.Configurations;

internal sealed class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable("delivery_methods", Schema.Orders);
        builder.HasKey(del => del.Id);
        builder.Property(del => del.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
