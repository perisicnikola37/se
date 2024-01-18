using System.ComponentModel.DataAnnotations;
using Vega.ValidationAttributes;

namespace Vega.Models;
public class Income
{

	public int Id { get; set; }
	[Required(ErrorMessage = "Description is required")]
	[StringLength(255, MinimumLength = 8, ErrorMessage = "Description must be between 8 and 255 characters")]
	public string Description { get; set; }
	[Required(ErrorMessage = "Amount is required")]
	[Minimum(0, ErrorMessage = "Amount must be greater than zero(0)")]
	public float Amount { get; set; }
	public DateTime Created_at { get; set; } = DateTime.Now;
	[Required(ErrorMessage = "IncomeGroupId is required")]

	public int IncomeGroupId { get; set; }

	public IncomeGroup? IncomeGroup { get; set; }

	public int UserId { get; set; }

	public User? User { get; set; }
}