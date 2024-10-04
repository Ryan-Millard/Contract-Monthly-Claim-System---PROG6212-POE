using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContractMonthlyClaimSystem.Models.Enums;

namespace ContractMonthlyClaimSystem.Models
{
	// Claims table in DB
	public class MonthlyClaim
	{
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		// Navigation property
		[ForeignKey("UserId")]
		public virtual User User { get; set; } // This creates a relationship with the User entity

		[Required]
		public Status Status { get; set; } = Status.Pending; // Pending, Approved, or Rejected

		[Required]
		public DateTime SubmissionDate { get; set; } = DateTime.Now;

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Hours worked must be greater than zero.")]
		public decimal HoursWorked { get; set; } = 0.0m;

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be greater than zero.")]
		public decimal HourlyRate { get; set; } = 0.0m;

		[NotMapped]
		public decimal Amount => HoursWorked * HourlyRate;

		[MaxLength(500)]
		public string? Description { get; set; }

		// Constructor
		public MonthlyClaim()
		{
			User = new User();
		}
	}
}
