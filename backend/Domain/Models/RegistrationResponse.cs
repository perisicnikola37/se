namespace Domain.Models;

public class RegistrationResponse
{
    public User User { get; set; }
    public string Token { get; set; }
}