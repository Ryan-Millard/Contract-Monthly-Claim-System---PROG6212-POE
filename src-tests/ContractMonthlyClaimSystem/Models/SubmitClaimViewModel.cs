using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Validation;

namespace ContractMonthlyClaimSystem.Models
{
    public class SubmitClaimViewModel
    {
        private const double MaxHoursWorked = 8 * 5 * 4; // 160 max hours per month

        [BindProperty]
        [Required(ErrorMessage = "Please select a course.")]
        public string SelectedCourse { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Please enter hours worked.")]
        [Range(0.5, MaxHoursWorked, ErrorMessage = "Please enter a number of hours between 0.5 and 160.")]
        public decimal HoursWorked { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter hourly rate.")]
        [Range(150, 500, ErrorMessage = "Please enter an hourly rate between 150 and 500.")]
        public decimal HourlyRate { get; set; }

        public decimal TotalAmount => HoursWorked > 0 && HourlyRate > 0 ? HoursWorked * HourlyRate : 0;

        [BindProperty]
        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please upload supporting documents.")]
        [DataType(DataType.Upload)]
        public IFormFileCollection SupportingDocuments { get; set; }

        public List<SelectListItem> Courses { get; set; } = new List<SelectListItem>();
    }
}

