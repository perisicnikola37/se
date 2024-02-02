using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
	public enum AccountTypes
	{
		Regular,
		Administrator
	}

	private string accountType;

	[JsonIgnore]
	public AccountTypes AccountTypeEnum
	{
		get
		{
			Enum.TryParse(accountType, out AccountTypes result);
			return result;
		}
		set => accountType = value.ToString();
	}

	[Required(ErrorMessage = "AccountType is required")]
	[RegularExpression("^(Regular|Administrator)$", ErrorMessage = "Invalid AccountType")]
	public string AccountType
	{
		get => accountType;
		set
		{
			if (Enum.TryParse(value, out AccountTypes result))
				accountType = value;
			else
				throw new ArgumentException("Invalid AccountType");
		}
	}

	public int Id { get; set; }
	public string Username { get; set; }

	// old custom validation
	// [EmailValidation(ErrorMessage = "Please enter a valid email address.")]
	public string Email { get; set; }
	[JsonIgnore]
	public string Password { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public List<Expense>? Expenses { get; set; }
	public List<Income>? Incomes { get; set; }
	public List<IncomeGroup>? IncomeGroups { get; set; }
	public List<ExpenseGroup>? ExpenseGroups { get; set; }
	public List<Blog>? Blogs { get; set; }
}