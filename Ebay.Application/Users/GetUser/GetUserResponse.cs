namespace Ebay.Application.Users.GetUser;

public sealed record GetUserResponse(
    string FirstName,
    string LastName,
    string Username,
    string PhoneNo,
    string Email
    );
