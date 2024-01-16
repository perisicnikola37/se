public class Reminder
{
    public int Id { get; set; }
    public string Reminder_day { get; set; }
    public string Type { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public Boolean Active { get; set; }
}