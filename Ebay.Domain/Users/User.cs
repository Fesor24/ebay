using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Users;

public sealed class User : AuditableEntity
{
    public User(Guid id) : base(id)
    {

    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNo { get; private set; }
    public Address Address { get; private set; }
    //public Guid WishlistId {  get; private set; }   

    public static User Create(Guid userId, string firstName, string lastName, string email, string phoneNo,
        string street, string city, string country, string postCode)
    {
        User user = new(userId)
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNo = phoneNo,
            Address = new(street, city, country, postCode),
        };

        return user;
    }
}
