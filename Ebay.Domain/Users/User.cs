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
    public string Username {  get; private set; }
    public string PhoneNo { get; private set; }
    public Address Address { get; private set; }
    public string Password { get; private set; }
    //public Guid WishlistId {  get; private set; }   
    public Role Role { get; private set; }

    public static User Create(Guid userId, string firstName, string lastName, string email, string phoneNo,
        string street, string city, string country, string postCode, string password, string username)
    {
        User user = new(userId)
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNo = phoneNo,
            Address = new(street, city, country, postCode),
            Username = username
        };

        return user;
    }

    public static User Create(Guid userId, string firstName, string lastName, string email, string phoneNo,
      string password, string username)
    {
        User user = new(userId)
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNo = phoneNo,
            Address = new("", "", "", ""),
            Username = username
        };

        return user;
    }

    public void SetPassword(string hashedPassword)
    {
        Password = hashedPassword;
    }
}
