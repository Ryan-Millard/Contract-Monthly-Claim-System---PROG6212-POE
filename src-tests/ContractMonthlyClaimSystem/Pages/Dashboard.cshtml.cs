using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Pages
{
	public class DashboardModel : PageModel
	{
		[BindProperty]
		public LecturerDashboardViewModel? LecturerDashboard { get; set; }

		public void OnGet()
		{
			// Initialize any data needed for the view, e.g., load courses
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				// Redisplay the form with validation errors
				return Page();
			}

			// Process valid form data here (e.g., save to the database)
			// Redirect or show a success message
			return RedirectToPage("/Dashboard");
		}
	}
}
