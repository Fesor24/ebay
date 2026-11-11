using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Users;

public sealed class Permission : Entity
{
    public string Name {  get; private set; }
    public string Code {  get; private set; }
}
