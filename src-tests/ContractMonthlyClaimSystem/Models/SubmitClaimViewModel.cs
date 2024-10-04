using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Models
{
	public class SubmitClaimViewModel
	{
		[BindProperty]
		[Required(ErrorMessage = "Please select a course.")]
		public string SelectedCourse { get; set; } = "";

		[BindProperty]
		[Required(ErrorMessage = "Please enter hours worked.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid number of hours.")]
		public decimal HoursWorked { get; set; } = 0;

		[BindProperty]
		[Required(ErrorMessage = "Please enter the hourly rate.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid hourly rate.")]
		public decimal HourlyRate { get; set; } = 0;

		[BindProperty]
		[Required(ErrorMessage = "Total amount is required.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid total amount.")]
		public decimal TotalAmount { get; set; } = 0;

		[BindProperty]
		[Required(ErrorMessage = "Please provide a description.")]
		[MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
		public string Description { get; set; } = "";

		[BindProperty]
		[Required(ErrorMessage = "Please upload at least one supporting document.")]
		[FileExtensions(Extensions = ".pdf,.docx,.xlsx", ErrorMessage = "Only PDF, DOCX, and XLSX files are allowed.")]
		public IFormFileCollection SupportingDocuments { get; set; } = new FormFileCollection();

		public List<SelectListItem> Courses { get; set; } = new List<SelectListItem>
		{
			new SelectListItem { Value = "Course1", Text = "Course 1" },
			new SelectListItem { Value = "Course2", Text = "Course 2" },
			new SelectListItem { Value = "Course3", Text = "Course 3" }
		};
	}
}
