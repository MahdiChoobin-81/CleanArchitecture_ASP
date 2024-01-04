using System.ComponentModel.DataAnnotations;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Movie_asp.Entities;

public class User
{
    public Id Id { get; private set; }
    [MaxLength(50)]
    public FullName FullName { get; private set; }
    [MaxLength(30)]
    public Username Username { get; private set; }
    [MaxLength(30)]
    public Password Password { get; private set; }
    [MaxLength(100)]
    public Email Email { get; private set; }
    public CreatedAt CreatedAt { get; private set; }

    public User(Id id, FullName fullName, Username username, Password password, Email email, CreatedAt createdAt)
    {
        Id = id;
        FullName = fullName;
        Username = username;
        Password = password;
        Email = email;
        CreatedAt = createdAt;
    }

    public void Update(FullName fullName, Username username, Password password, Email email)
    {
        FullName = fullName;
        Username = username;
        Password = password;
        Email = email;
    }

}