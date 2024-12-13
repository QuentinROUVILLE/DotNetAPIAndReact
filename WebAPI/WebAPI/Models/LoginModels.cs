namespace WebAPI.Models;

public class LoginModel
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}

public class LoginResponse
{
    public required string Token { get; set; }
}