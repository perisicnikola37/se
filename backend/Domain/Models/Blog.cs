using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Blog
{
	public int Id { get; set; }
	[MaxLength(1500)]
	public string Description { get; set; }
	public string? Author { get; set; }
	public string Text { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public int UserId { get; set; }
	public User? User { get; set; }
}

