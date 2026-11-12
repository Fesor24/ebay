namespace Ebay.Infrastructure.Security;

public sealed class JwtSettings
{
    internal readonly static string NAME = nameof(JwtSettings);
    public string Key { get; set; } = "";
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public int ExpiryInMinutes {  get; set; }
}
