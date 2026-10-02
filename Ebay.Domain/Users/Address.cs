namespace Ebay.Domain.Users;

public sealed record Address(
    string Street,
    string City,
    string Country,
    string PostCode
    );
