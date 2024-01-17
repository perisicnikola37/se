public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccountType { get; set; }
    public string Password { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public List<Expense>? Expenses { get; set; } 
}
