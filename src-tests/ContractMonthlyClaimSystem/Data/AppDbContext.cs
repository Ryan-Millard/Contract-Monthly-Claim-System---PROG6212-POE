using ContractMonthlyClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication14;

public class AppDbContext : DbContext
{
	protected DbSet<User> User { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connectionString = "server=localhost;user=ryan;password=password;database=CMCS-PROG6021";

		var serverVersion = new MySqlServerVersion(new Version(10, 4, 32));
		optionsBuilder.UseMySql(connectionString, serverVersion,
				options => options.EnableRetryOnFailure());
	}
}
