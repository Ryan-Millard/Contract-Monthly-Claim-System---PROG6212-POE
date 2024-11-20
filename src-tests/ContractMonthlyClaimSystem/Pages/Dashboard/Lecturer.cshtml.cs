using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Pages.Dashboard
{
    public class LecturerModel : PageModel
    {
        private readonly AppDbContext _context;

        // Property to hold the list of claims
        public List<MonthlyClaim> Claims { get; set; } = new List<MonthlyClaim>();

        // Constructor to inject the AppDbContext
        public LecturerModel(AppDbContext context)
        {
            _context = context;
        }

		public async Task<IActionResult> OnGetAsync(string searchTerm)
		{
			var userRole = HttpContext.Session.GetString("UserRole");
			if (userRole == "HR")
			{
				return RedirectToPage("/Dashboard/HR");
			}
			else if (userRole == "Admin")
			{
				return RedirectToPage("/Dashboard/Admin");
			}
			else if (userRole != "Lecturer")
			{
				return RedirectToPage("/Users/Login");
			}

			// Fetch claims for the logged-in user
			var userId = int.Parse(HttpContext.Session.GetString("UserId"));

			// Start with the base query for claims
			var query = _context.Claims
				.Where(c => c.UserId == userId)  // Filter claims by the current user (Lecturer)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				Status? status = Enum.TryParse(searchTerm, true, out Status parsedStatus) ? parsedStatus : (Status?)null;

				// Filter claims based on the search term (either by description or status)
				query = query.Where(c =>
							c.Description.Contains(searchTerm) ||
							(status.HasValue && c.Status == status.Value)
						);
			}

			Claims = await query.ToListAsync();
			return Page();
		}
    }
}

