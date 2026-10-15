using Ebay.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebay.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products", Schema.Products);

        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.Money, moneyBuilder =>
        {
            moneyBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        //builder.HasMany(p => p.Assets)
        //  .WithOne(ass => ass.Product)
        //.HasForeignKey(ass => ass.Product);

        builder.HasOne(prd => prd.Category)
            .WithMany(cat => cat.Products)
            .HasForeignKey(prd => prd.CategoryId);

    }
}
