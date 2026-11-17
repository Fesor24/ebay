using Ebay.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebay.Infrastructure.Configurations;

internal sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("chats", Schema.Products);
        builder.HasKey(ch => ch.Id);
        builder.Property(ch => ch.Message)
            .HasMaxLength(250);
        builder.HasOne(ch => ch.Product)
            .WithMany(pr => pr.Chats)
            .HasForeignKey(ch => ch.ProductId);
        builder.HasOne(ch => ch.Sender)
            .WithOne();
        builder.HasOne(ch => ch.Recipient)
            .WithOne();
    }
}
