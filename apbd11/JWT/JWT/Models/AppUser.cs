namespace JWT.Models;

public class AppUser
{
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}
