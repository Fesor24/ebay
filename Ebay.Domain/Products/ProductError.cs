using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products;

public static class ProductError
{
    public static Error NotFound = new Error("PRODUCT.NOTFOUND", "Product not found");
}
