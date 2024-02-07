using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface ISeeder
{
	void Seed(ModelBuilder modelBuilder);
}
