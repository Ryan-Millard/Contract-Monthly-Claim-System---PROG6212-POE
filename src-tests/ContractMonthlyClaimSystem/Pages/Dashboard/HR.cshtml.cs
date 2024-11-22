using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Models.Enums;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ContractMonthlyClaimSystem.Pages.Dashboard
{
	public class HrModel : PageModel
	{
		private readonly AppDbContext _context;
		private readonly IReportService _reportService;  // Changed to interface

		public HrModel(AppDbContext context, IReportService reportService)  // Changed to interface
		{
			_context = context;
			_reportService = reportService;
		}

		// Rest of your existing code remains the same
		public IList<MonthlyClaim> ApprovedClaims { get; set; }
		public IList<User> Lecturers { get; set; }
		public string PaymentSummary { get; set; }

		public async Task<IActionResult> OnGetAsync()
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
			else if (userRole != "HR")
			{
				return RedirectToPage("/Users/Login");
			}

			ApprovedClaims = await _context.Claims
				.Where(c => c.Status == Status.Approved)
				.Include(c => c.User)
				.ToListAsync();
			Lecturers = await _context.User
				.Where(u => u.Role == Role.Lecturer)
				.ToListAsync();
			PaymentSummary = $"Total Claims: {ApprovedClaims.Count}, Total Amount: R{ApprovedClaims.Sum(c => c.TotalAmount).ToString("F2")}";
			return Page();
		}

		public async Task<IActionResult> OnPostProcessPaymentAsync(int claimId)
		{
			var claim = await _context.Claims.FindAsync(claimId);
			if (claim != null)
			{
				claim.Status = Status.Paid;
				await _context.SaveChangesAsync();
			}
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostUpdateLecturerAsync(int lecturerId, string firstName, string lastName, string email, Role role)
		{
			var lecturer = await _context.User.FindAsync(lecturerId);
			if (lecturer != null)
			{
				lecturer.FirstName = firstName;
				lecturer.LastName = lastName;
				lecturer.Email = email;
				lecturer.Role = role;
				await _context.SaveChangesAsync();
			}
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostGenerateReportAsync(string format, string status)
		{
			// Parse the status string to the Status enum
			Status claimStatus;
			if (!Enum.TryParse(status, true, out claimStatus))
			{
				return Content("Invalid status selected.");
			}

			// Filter claims based on the selected status
			var claims = await _context.Claims
				.Where(c => c.Status == claimStatus)
				.Include(c => c.User)
				.ToListAsync();

			if (!claims.Any())
			{
				return Content($"No {claimStatus} claims found.");
			}

			// Parse the format string to enum
			ReportFormat reportFormat = format.ToUpper() == "EXCEL"
				? ReportFormat.Excel
				: ReportFormat.PDF;

			// Generate report with selected format on a new thread
			byte[] reportBytes = await Task.Run(() => _reportService.GenerateClaimReport(claims, reportFormat));

			// Set appropriate content type and file extension
			string contentType = reportFormat == ReportFormat.PDF
				? "application/pdf"
				: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

			string fileName = reportFormat == ReportFormat.PDF
				? $"{status}ClaimReport.pdf"
				: $"{status}ClaimReport.xlsx";

			return File(reportBytes, contentType, fileName);
		}

		public async Task<IActionResult> OnPostGenerateInvoiceAsync(string format, int lecturerId)
		{
			// Find the lecturer by ID
			var lecturer = await _context.User.FindAsync(lecturerId);
			if (lecturer == null)
			{
				return Content("Invalid lecturer selected.");
			}

			// Get claims for the selected lecturer
			var lecturerClaims = await _context.Claims
				.Where(c => c.UserId == lecturerId && c.Status == Status.Approved)
				.ToListAsync();

			if (!lecturerClaims.Any())
			{
				return Content($"No approved claims found for {lecturer.FullName}.");
			}

			// Parse the format string to enum
			ReportFormat invoiceFormat = format.ToUpper() == "EXCEL"
				? ReportFormat.Excel
				: ReportFormat.PDF;

			// Generate invoice for the lecturer
			byte[] invoiceBytes = await Task.Run(() => _reportService.GenerateInvoice(lecturer, lecturerClaims, invoiceFormat));

			// Set content type and file extension
			string contentType = invoiceFormat == ReportFormat.PDF
				? "application/pdf"
				: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

			string fileName = invoiceFormat == ReportFormat.PDF
				? $"{lecturer.FullName.Replace(" ", "_")}_Invoice.pdf"
				: $"{lecturer.FullName.Replace(" ", "_")}_Invoice.xlsx";

			return File(invoiceBytes, contentType, fileName);
		}

    }
}
