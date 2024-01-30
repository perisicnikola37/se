namespace Domain.Models;

// TODO goes to DTO
public class RegistrationResponse
{
    public User User { get; set; }
    public string Token { get; set; }
}