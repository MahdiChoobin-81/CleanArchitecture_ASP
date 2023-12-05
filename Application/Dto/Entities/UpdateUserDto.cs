namespace Application.Dto.Entities;

public class UpdateUserDto
{
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}