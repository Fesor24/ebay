using Ebay.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebay.Infrastructure.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Domain.Orders.Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders", Schema.Orders);
        builder.HasKey(ord => ord.Id);
        builder.OwnsOne(ord => ord.ShippingAddress, sa =>
        {
            sa.Property(s => s.Street).HasColumnName("street");
            sa.Property(s => s.City).HasColumnName("city");
            sa.Property(s => s.State).HasColumnName("state");
            sa.Property(s => s.PostCode).HasColumnName("post_code");
        });
        builder.HasMany(ord => ord.Items)
            .WithOne()
            .HasForeignKey()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(ord => ord.BuyerEmail)
            .IsRequired();
        builder.Property(ord => ord.Status)
            .HasConversion(ordStatus => ordStatus.ToString(),
            ordStatusString => Enum.Parse<OrderStatus>(ordStatusString));
        builder.Property(ord => ord.PaymentStatus)
            .HasConversion(paymentStatus => paymentStatus.ToString(),
            paymentStatusString => Enum.Parse<PaymentStatus>(paymentStatusString));
    }
}
