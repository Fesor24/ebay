namespace Ebay.Domain.Products;

public record Currency
{
    public static readonly Currency Usd = new("$");
    public static readonly Currency Gbp = new("£");
    private Currency(string currencyCode) => currencyCode = Code;
    public string Code { get; init; }

}
