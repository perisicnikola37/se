public class Blog
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
}