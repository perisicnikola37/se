using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vega.Models
{
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
        {
          accountType = value;
        }
        else
        {
          throw new ArgumentException("Invalid AccountType");
        }
      }
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public List<Expense>? Expenses { get; set; }
    public List<Income>? Incomes { get; set; }
  }
}
