using Ebay.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebay.Infrastructure.Configurations;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items", Schema.Orders);
        builder.HasKey(ord => ord.Id);
        builder.Property(ord => ord.ProductInfo)
            .HasColumnType("jsonb");
    }
}
