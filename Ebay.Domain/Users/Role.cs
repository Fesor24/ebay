using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Users;

public sealed class Role : Entity
{
    public string Name { get; private set; }
    public string Code {  get; private set; }
}

public static class DbRole
{
    public static readonly string ADMIN = nameof(ADMIN).ToLower();
    public static readonly string USER = nameof(USER).ToLower();
}
