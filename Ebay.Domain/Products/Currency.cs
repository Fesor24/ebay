namespace Ebay.Domain.Products;

public record Currency
{
    public static readonly Currency Usd = new("$");
    public static readonly Currency Gbp = new("£");
    private Currency(string currencyCode) => Code = currencyCode;
    public string Code { get; init; }
    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
            throw new ApplicationException("The currency code is invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = [Usd, Gbp];
}
