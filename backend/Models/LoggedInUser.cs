namespace Vega.Models;
public class LoggedInUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccountType { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public string Token { get; set; }
}
