namespace Ebay.Domain.Orders;

public sealed record Address(
    string Street,
    string City,
    string State,
    string PostCode
    );
