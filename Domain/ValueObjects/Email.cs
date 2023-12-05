using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.RegularExpressions;
using FluentResults;

namespace Movie_asp.ValueObjects;

// [ComplexType]
public record Email
{
    public string Value { get; }

    private Email(string email)
    {
        Value = email;
    }
    public static Result<Email?> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
                    return Result.Fail("Email cannot be empty.");
        
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (!match.Success)
        {
            return Result.Fail("Email Address is in a wrong pattern.");
        }
    
        
    
        return new Email(email);
    }
}