using System.ComponentModel.DataAnnotations;
using Movie_asp.ValueObjects;

namespace Movie_asp.Entities;

public class User
{
    public UserId Id { get; private set; }
    public UserFullName FullName { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }
    public CreatedAt CreatedAt { get; private set; }

    public User(UserId id, UserFullName fullName, Username username, Password password, Email email, CreatedAt createdAt)
    {
        Id = id;
        FullName = fullName;
        Username = username;
        Password = password;
        Email = email;
        CreatedAt = createdAt;
    
    }

    public void Update(UserFullName userFullName, Username username, Password password, Email email)
    {
        FullName = userFullName;
        Username = username;
        Password = password;
        Email = email;
    }
    
    
    



}