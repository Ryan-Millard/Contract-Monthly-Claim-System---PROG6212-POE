using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Pages.Dashboard
{
	public class ClaimDetailsModel : PageModel
	{
		private readonly AppDbContext _context;

		// Property to hold the claim details
		public MonthlyClaim Claim { get; set; }

		// Constructor to inject the AppDbContext
		public ClaimDetailsModel(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet(int id)
		{
			// Fetch the claim by ID
			Claim = _context.Claims
				.Include(c => c.SupportingDocuments) // Include supporting documents if needed
				.FirstOrDefault(c => c.Id == id);

			if (Claim == null)
			{
				return NotFound(); // Return a 404 if the claim is not found
			}

			return Page();
		}
	}
}
