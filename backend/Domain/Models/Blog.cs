using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Blog
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Description must be between 8 and 255 characters")]
    public string Description { get; set; }

    public string Author { get; set; }

    [Required(ErrorMessage = "Text is required")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "Text must be between 2 and 1255 characters")]
    public string Text { get; set; }

    public DateTime Created_at { get; set; } = DateTime.Now;
    public int UserId { get; set; }

    public User? User { get; set; }
}