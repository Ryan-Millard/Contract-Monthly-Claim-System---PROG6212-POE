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

        public async Task OnGetAsync()
        {
            ApprovedClaims = await _context.Claims
                .Where(c => c.Status == Status.Approved)
                .Include(c => c.User)
                .ToListAsync();
            Lecturers = await _context.User.ToListAsync();
            PaymentSummary = $"Total Claims: {ApprovedClaims.Count}, Total Amount: {ApprovedClaims.Sum(c => c.TotalAmount).ToString("F2")}";
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

		public async Task<IActionResult> OnPostGenerateReportAsync(string format)
		{
			var claims = await _context.Claims
				.Where(c => c.Status == Status.Approved)
				.Include(c => c.User)
				.Select(c => new { c.User.FirstName, c.User.LastName, c.HoursWorked, c.TotalAmount })
				.ToListAsync();

			if (!claims.Any())
			{
				return Content("No approved claims found.");
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
				? "ClaimReport.pdf"
				: "ClaimReport.xlsx";

			return File(reportBytes, contentType, fileName);
		}
    }
}
