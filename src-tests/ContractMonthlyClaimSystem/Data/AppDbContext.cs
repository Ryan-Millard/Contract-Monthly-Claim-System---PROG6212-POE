using ContractMonthlyClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Data;

public class AppDbContext : DbContext
{
	public DbSet<User> User { get; set; }

	public AppDbContext(DbContextOptions options) : base(options)
	{
	}
}
