using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContractMonthlyClaimSystem.Models.Users
{
	public class LogoutModel : PageModel
	{
		public async Task<IActionResult> OnGetAsync()
		{
			// Clear the session data
			HttpContext.Session.Remove("UserFirstName");
			HttpContext.Session.Remove("UserLastName");
			HttpContext.Session.Remove("UserEmail");

			// Sign out the user and clear claims
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Set temporary data for a popup message on the next page
			TempData["ModalPopUpHeading"] = "Logout Notification";
			TempData["ModalPopUpMessage"] = "You have been logged out successfully.";

			return RedirectToPage("/Index");
		}
	}
}
