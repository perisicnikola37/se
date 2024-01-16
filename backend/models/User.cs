using System.Security.Cryptography;
using System.Text;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    private string hashedPassword; 

    public string Password
    {
        get { return hashedPassword; }
        set { hashedPassword = HashPassword(value); }
    }

    public DateTime Created_at { get; set; } = DateTime.Now;

    // SHA-256
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
