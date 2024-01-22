using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class ExpenseGroup
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 255 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 255 characters")]
    public required string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Expense>? Expenses { get; set; }
}