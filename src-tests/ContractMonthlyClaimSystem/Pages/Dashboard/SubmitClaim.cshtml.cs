using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractMonthlyClaimSystem.Pages.Dashboard
{
	public class SubmitClaimModel : PageModel
	{
		private readonly AppDbContext _context;

		public SubmitClaimModel(AppDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public SubmitClaimViewModel ClaimViewModel { get; set; } = new SubmitClaimViewModel();

		public void OnGet()
		{
			// Populate courses or other necessary data
			ClaimViewModel.Courses = GetCourses(); // Assuming you have a method to fetch courses
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				// Repopulate the courses if model validation fails
				ClaimViewModel.Courses = GetCourses();
				return Page();
			}

			// Create the MonthlyClaim object
			var claim = new MonthlyClaim
			{
				UserId = GetUserId(), // Replace with the actual logic to get the UserId
				HoursWorked = ClaimViewModel.HoursWorked,
				HourlyRate = ClaimViewModel.HourlyRate,
				Description = ClaimViewModel.Description,
				SubmissionDate = DateTime.Now,
				Status = Status.Pending
			};

			// Save the claim to the database
			_context.Claims.Add(claim);
			await _context.SaveChangesAsync();

			// Handle file uploads if any
			await UploadSupportingDocuments(claim.Id, ClaimViewModel.SupportingDocuments); // Create this method to handle uploads

			// Redirect or return a confirmation view
			return RedirectToPage("Success"); // Change to your success page
		}

		private List<SelectListItem> GetCourses()
		{
			// Implement your logic to fetch the list of courses
			return new List<SelectListItem>
			{
				new SelectListItem { Value = "Course1", Text = "Course 1" },
				new SelectListItem { Value = "Course2", Text = "Course 2" },
				new SelectListItem { Value = "Course3", Text = "Course 3" }
			};
		}

		private int GetUserId()
		{
			// Implement logic to get the current user's ID
			// Placeholder for demonstration; replace with actual user ID retrieval logic
			return 1; 
		}

		private async Task UploadSupportingDocuments(int claimId, IFormFileCollection files)
		{
			// Implement logic to save the files to the server or database
			foreach (var file in files)
			{
				if (file.Length > 0)
				{
					// Define the path where files will be saved (adjust as necessary)
					var directoryPath = Path.Combine("wwwroot/uploads", claimId.ToString());
					var filePath = Path.Combine(directoryPath, file.FileName);

					// Create the directory if it doesn't exist
					if (!Directory.Exists(directoryPath))
					{
						Directory.CreateDirectory(directoryPath);
					}

					// Save the file
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
				}
			}
		}
	}
}

