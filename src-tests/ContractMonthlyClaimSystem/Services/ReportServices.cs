using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Models.Enums;

namespace ContractMonthlyClaimSystem.Services
{
	public interface IReportService
	{
		byte[] GenerateClaimReport(List<MonthlyClaim> claims, ReportFormat format = ReportFormat.PDF);
		byte[] GenerateInvoice(User lecturer, List<MonthlyClaim> claims, ReportFormat format = ReportFormat.PDF);
	}

	public class ReportService : IReportService
	{
		private readonly CultureInfo zaCulture = new CultureInfo("en-ZA");

		public byte[] GenerateClaimReport(List<MonthlyClaim> claims, ReportFormat format = ReportFormat.PDF)
		{
			return format switch
			{
				ReportFormat.PDF => GeneratePdfReport(claims),
					ReportFormat.Excel => GenerateExcelReport(claims),
					_ => throw new ArgumentException("Unsupported format", nameof(format))
			};
		}

		public byte[] GenerateInvoice(User lecturer, List<MonthlyClaim> claims, ReportFormat format = ReportFormat.PDF)
		{
			return format switch
			{
				ReportFormat.PDF => GeneratePdfInvoice(lecturer, claims),
					ReportFormat.Excel => GenerateExcelInvoice(lecturer, claims),
					_ => throw new ArgumentException("Unsupported format", nameof(format)),
			};
		}

		private byte[] GeneratePdfReport(List<MonthlyClaim> claims)
		{
			using var memoryStream = new MemoryStream();
			var writer = new PdfWriter(memoryStream);
			var pdf = new PdfDocument(writer);
			var document = new Document(pdf);

			// Add title
			document.Add(new Paragraph("Monthly Claims Report")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(20));

			// Create table
			var table = new Table(5).UseAllAvailableWidth();

			// Add headers
			table.AddCell(new Cell().Add(new Paragraph(("Lecturer"))));
			table.AddCell(new Cell().Add(new Paragraph("Course")));
			table.AddCell(new Cell().Add(new Paragraph("Hourly Rate")));
			table.AddCell(new Cell().Add(new Paragraph("Hours Worked")));
			table.AddCell(new Cell().Add(new Paragraph("Total Amount")));

			// Add data rows
			foreach (var claim in claims)
			{
				table.AddCell(new Cell().Add(new Paragraph($"{claim.User?.FirstName} {claim.User?.LastName}")));
				table.AddCell(new Cell().Add(new Paragraph(claim.Course.ToString())));
				table.AddCell(new Cell().Add(new Paragraph(claim.HourlyRate.ToString("C", zaCulture))));
				table.AddCell(new Cell().Add(new Paragraph(claim.HoursWorked.ToString("F2"))));
				table.AddCell(new Cell().Add(new Paragraph(claim.TotalAmount.ToString("C", zaCulture))));
			}

			// Add summary section
			document.Add(table);
			document.Add(new Paragraph($"\nTotal Claims: {claims.Count}")
					.SetTextAlignment(TextAlignment.LEFT));
			document.Add(new Paragraph($"Total Amount: {claims.Sum(c => c.TotalAmount).ToString("C", zaCulture)}")
					.SetTextAlignment(TextAlignment.LEFT));

			document.Close();
			return memoryStream.ToArray();
		}

		private byte[] GenerateExcelReport(List<MonthlyClaim> claims)
		{
			using var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("Monthly Claims");

			// Add title
			worksheet.Cell("A1").Value = "Monthly Claims Report";
			worksheet.Range("A1:E1").Merge().Style.Font.SetBold().Font.SetFontSize(14);

			// Add headers
			worksheet.Cell("A2").Value = "Lecturer";
			worksheet.Cell("B2").Value = "Course";
			worksheet.Cell("C2").Value = "Hourly Rate";
			worksheet.Cell("D2").Value = "Hours Worked";
			worksheet.Cell("E2").Value = "Total Amount";

			worksheet.Range("A2:E2").Style.Font.SetBold();

			// Add data rows
			var row = 3;
			foreach (var claim in claims)
			{
				worksheet.Cell($"A{row}").Value = $"{claim.User?.FirstName} {claim.User?.LastName}";
				worksheet.Cell($"B{row}").Value = claim.HourlyRate;
				worksheet.Cell($"C{row}").Value = claim.Course.ToString();
				worksheet.Cell($"D{row}").Value = claim.HoursWorked;
				worksheet.Cell($"E{row}").Value = claim.TotalAmount;
				worksheet.Cell($"E{row}").Style.NumberFormat.Format = "R #,##0.00";
				row++;
			}

			// Add summary section
			row++;
			worksheet.Cell($"A{row}").Value = "Total Claims:";
			worksheet.Cell($"B{row}").Value = claims.Count;

			row++;
			worksheet.Cell($"A{row}").Value = "Total Amount:";
			worksheet.Cell($"B{row}").Value = claims.Sum(c => c.TotalAmount);
			worksheet.Cell($"B{row}").Style.NumberFormat.Format = "R #,##0.00";

			// Format the table
			var tableRange = worksheet.Range($"A2:E{row}");
			tableRange.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
			tableRange.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

			// Auto-fit columns
			worksheet.Columns().AdjustToContents();

			using var memoryStream = new MemoryStream();
			workbook.SaveAs(memoryStream);
			return memoryStream.ToArray();
		}

		private byte[] GeneratePdfInvoice(User lecturer, List<MonthlyClaim> claims)
		{
			using var memoryStream = new MemoryStream();
			var writer = new PdfWriter(memoryStream);
			var pdf = new PdfDocument(writer);
			var document = new Document(pdf);

			// Add invoice title
			document.Add(new Paragraph("Invoice")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(20));

			// Add lecturer details
			document.Add(new Paragraph($"Lecturer: {lecturer.FullName}")
					.SetTextAlignment(TextAlignment.LEFT)
					.SetFontSize(12));
			document.Add(new Paragraph($"Email: {lecturer.Email}")
					.SetTextAlignment(TextAlignment.LEFT)
					.SetFontSize(12));
			document.Add(new Paragraph($"Date: {DateTime.Now.ToString("dd MMMM yyyy", zaCulture)}")
					.SetTextAlignment(TextAlignment.LEFT)
					.SetFontSize(12));

			// Group claims by course and calculate totals
			var groupedClaims = claims.GroupBy(c => c.Course)
				.Select(g => new
						{
						Course = g.Key,
						TotalHoursWorked = g.Sum(c => c.HoursWorked),
						HourlyRate = g.First().HourlyRate, // Assuming the rate is the same for the course
						TotalAmount = g.Sum(c => c.TotalAmount)
						}).ToList();

			// Add table for claims
			var table = new Table(4).UseAllAvailableWidth();

			// Add headers
			table.AddCell(new Cell().Add(new Paragraph("Course")));
			table.AddCell(new Cell().Add(new Paragraph("Hours Worked")));
			table.AddCell(new Cell().Add(new Paragraph("Hourly Rate")));
			table.AddCell(new Cell().Add(new Paragraph("Total Amount")));

			// Add aggregated data rows
			foreach (var groupedClaim in groupedClaims)
			{
				table.AddCell(new Cell().Add(new Paragraph(groupedClaim.Course.ToString())));
				table.AddCell(new Cell().Add(new Paragraph(groupedClaim.TotalHoursWorked.ToString("F2"))));
				table.AddCell(new Cell().Add(new Paragraph(groupedClaim.HourlyRate.ToString("C", zaCulture))));
				table.AddCell(new Cell().Add(new Paragraph(groupedClaim.TotalAmount.ToString("C", zaCulture))));
			}

			document.Add(table);

			// Add total amount due
			document.Add(new Paragraph($"\nTotal Due: {groupedClaims.Sum(g => g.TotalAmount).ToString("C", zaCulture)}")
					.SetTextAlignment(TextAlignment.RIGHT)
					.SetFontSize(12));

			document.Close();
			return memoryStream.ToArray();
		}

		private byte[] GenerateExcelInvoice(User lecturer, List<MonthlyClaim> claims)
		{
			using var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("Invoice");

			// Add title
			worksheet.Cell("A1").Value = "Invoice";
			worksheet.Range("A1:D1").Merge().Style.Font.SetBold().Font.SetFontSize(14);

			// Add lecturer details
			worksheet.Cell("A2").Value = $"Lecturer: {lecturer.FullName}";
			worksheet.Cell("A3").Value = $"Email: {lecturer.Email}";
			worksheet.Cell("A4").Value = $"Date: {DateTime.Now.ToString("dd MMMM yyyy", zaCulture)}";

			// Add headers
			worksheet.Cell("A6").Value = "Course";
			worksheet.Cell("B6").Value = "Hourly Rate";
			worksheet.Cell("C6").Value = "Hours Worked";
			worksheet.Cell("D6").Value = "Total Amount";
			worksheet.Range("A6:D6").Style.Font.SetBold();

			// Group claims by course and calculate totals
			var groupedClaims = claims.GroupBy(c => c.Course)
				.Select(g => new
						{
						Course = g.Key,
						TotalHoursWorked = g.Sum(c => c.HoursWorked),
						HourlyRate = g.First().HourlyRate,
						TotalAmount = g.Sum(c => c.TotalAmount)
						}).ToList();

			// Add aggregated data rows
			var row = 7;
			foreach (var groupedClaim in groupedClaims)
			{
				worksheet.Cell($"A{row}").Value = groupedClaim.Course.ToString();
				worksheet.Cell($"B{row}").Value = groupedClaim.HourlyRate;
				worksheet.Cell($"C{row}").Value = groupedClaim.TotalHoursWorked;
				worksheet.Cell($"D{row}").Value = groupedClaim.TotalAmount;
				worksheet.Cell($"D{row}").Style.NumberFormat.Format = "R #,##0.00";
				row++;
			}

			// Add total amount due
			worksheet.Cell($"C{row}").Value = "Total Due:";
			worksheet.Cell($"D{row}").Value = groupedClaims.Sum(g => g.TotalAmount);
			worksheet.Cell($"D{row}").Style.NumberFormat.Format = "R #,##0.00";

			// Format table
			var tableRange = worksheet.Range($"A6:D{row}");
			tableRange.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
			tableRange.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

			// Auto-fit columns
			worksheet.Columns().AdjustToContents();

			using var memoryStream = new MemoryStream();
			workbook.SaveAs(memoryStream);
			return memoryStream.ToArray();
		}
	}
}
