using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractMonthlyClaimSystem.Models
{
	public class LecturerDashboardViewModel
	{
		public decimal? HoursWorked { get; set; }
		public decimal? HourlyRate { get; set; }
		public decimal? TotalAmount { get; set; }
		public string? Description { get; set; }
		public List<SelectListItem>? Courses { get; set; } = new List<SelectListItem>
		{
			new SelectListItem { Value = "Course1", Text = "Course 1" },
			new SelectListItem { Value = "Course2", Text = "Course 2" },
			new SelectListItem { Value = "Course3", Text = "Course 3" },
		};
		public string? SelectedCourse { get; set; }
	}
}
