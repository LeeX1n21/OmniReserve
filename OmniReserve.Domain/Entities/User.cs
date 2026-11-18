using OmniReserve.Domain.Constants;
using OmniReserve.Domain.ValueObjects;

namespace OmniReserve.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public string PasswordHash { get; private set; }
    
    // Nueva propiedad de rol
    public string Role { get; private set; }

    public User(string name, EmailAddress email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        
        Role = Roles.Client;
    }

    public void PromoteToAdmin()
    {
        Role = Roles.Admin;
    }
}