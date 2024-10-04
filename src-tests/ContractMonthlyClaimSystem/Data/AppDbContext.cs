using ContractMonthlyClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Data;

public class AppDbContext : DbContext
{
	public DbSet<User> User { get; set; }
	public DbSet<MonthlyClaim> Claims { get; set; }

	public AppDbContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<MonthlyClaim>()
			.HasOne(c => c.User)       // Configure the Claim to User relationship
			.WithMany()                // User can have many Claims
			.HasForeignKey(c => c.UserId) // Specify the FK property
			.OnDelete(DeleteBehavior.Cascade); // Set the delete behavior
	}
}
