using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class IncomeGroup
{
	public int Id { get; set; }
	[Required(ErrorMessage = "Name is required")]
	[StringLength(255, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 255 characters")]
	public string Name { get; set; }
	[Required(ErrorMessage = "Description is required")]
	[StringLength(255, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 255 characters")]
	public string Description { get; set; }
	public DateTime Created_at { get; set; } = DateTime.Now;
	public List<Income>? Incomes { get; set; }
}