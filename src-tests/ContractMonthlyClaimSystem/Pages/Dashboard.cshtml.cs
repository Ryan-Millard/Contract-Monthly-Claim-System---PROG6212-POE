using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ContractMonthlyClaimSystem.Pages
{
	[Route("Dashboard")]
	public class DashboardModel : PageModel
	{
		public IActionResult OnGet()
		{
			var userRole = HttpContext.Session.GetString("UserRole");

			if (userRole == "Lecturer")
			{
				return RedirectToPage("/Dashboard/Lecturer");
			}
			else if (userRole == "Admin")
			{
				return RedirectToPage("/Dashboard/Admin");
			}
			else if (userRole == "HR")
			{
				return RedirectToPage("/Dashboard/HR");
			}

			TempData["ModalPopUpHeading"] = "Not Logged In";
			TempData["ModalPopUpMessage"] = "You need to be logged in to view that page, so you have been redirected to the Login page.";
			// Default action if the user has no matching role
			return RedirectToPage("/Users/Login");
		}
	}
}
