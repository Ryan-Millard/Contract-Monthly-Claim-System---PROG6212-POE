using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
	[BindProperties]
	public class LoginModel : PageModel
	{
		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; } = "";

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = "";

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string Email { get; set; } = "";

		public string successMessage = "";
		public string errorMessage = "";

		public void OnGet()
		{
		}

		public void OnPost()
		{
			if(!ModelState.IsValid)
			{
				errorMessage = "Login failed";
				return;
			}

			successMessage = "You have been logged in successfully";

			FirstName = "";
			LastName = "";
			Email = "";

			ModelState.Clear();
		}
	}
}
