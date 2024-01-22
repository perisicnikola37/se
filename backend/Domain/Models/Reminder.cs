using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Reminder
{
    public enum WeekDays
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    private string reminderDay;

    [JsonIgnore]
    public WeekDays ReminderDayEnum
    {
        get
        {
            Enum.TryParse(reminderDay, out WeekDays result);
            return result;
        }
        set => reminderDay = value.ToString();
    }

    [Required(ErrorMessage = "Reminder_day is required")]
    [RegularExpression("^(Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday)$",
        ErrorMessage = "Invalid Reminder_day")]
    public string ReminderDay
    {
        get => reminderDay;
        set
        {
            if (Enum.TryParse(value, out WeekDays result))
                reminderDay = value;
            else
                throw new ArgumentException("Invalid Reminder_day");
        }
    }

    public int Id { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool Active { get; set; }
}