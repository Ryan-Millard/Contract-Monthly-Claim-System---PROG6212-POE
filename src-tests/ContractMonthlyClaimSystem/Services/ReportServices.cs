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
            var table = new Table(3).UseAllAvailableWidth();
            
            // Add headers
            table.AddCell(new Cell().Add(new Paragraph("Lecturer")));
            table.AddCell(new Cell().Add(new Paragraph("Hours Worked")));
            table.AddCell(new Cell().Add(new Paragraph("Total Amount")));

            // Add data rows
            foreach (var claim in claims)
            {
                table.AddCell(new Cell().Add(new Paragraph($"{claim.User?.FirstName} {claim.User?.LastName}")));
                table.AddCell(new Cell().Add(new Paragraph(claim.HoursWorked.ToString("C", zaCulture))));
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
            worksheet.Range("A1:C1").Merge().Style.Font.SetBold().Font.SetFontSize(14);

            // Add headers
            worksheet.Cell("A2").Value = "Lecturer";
            worksheet.Cell("B2").Value = "Hours Worked";
            worksheet.Cell("C2").Value = "Total Amount";
            
            worksheet.Range("A2:C2").Style.Font.SetBold();

            // Add data rows
            var row = 3;
            foreach (var claim in claims)
            {
                worksheet.Cell($"A{row}").Value = $"{claim.User?.FirstName} {claim.User?.LastName}";
                worksheet.Cell($"B{row}").Value = claim.HoursWorked;
                worksheet.Cell($"C{row}").Value = claim.TotalAmount;
                worksheet.Cell($"C{row}").Style.NumberFormat.Format = "R #,##0.00";
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
            var tableRange = worksheet.Range($"A2:C{row}");
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
