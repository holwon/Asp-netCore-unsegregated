namespace __JWT.Models;
public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string? Email { get; set; }
    // public required List<string> Roles { get; set; }
    public required string Role { get; set; }
    public string? Surname { get; set; }// = 姓 = last name
    public string? GivenName { get; set; }// = 名 = first name
}
