using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Middleware;
using ContractMonthlyClaimSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ContractMonthlyClaimSystem.Data;
using iText.Kernel.Pdf;
using iText.IO.Source;

namespace ContractMonthlyClaimSystem
{
	public class Program
	{
		public static void Main(String[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();

			// Register the AppDbContext with the MySQL configuration
			builder.Services.AddDbContext<AppDbContext>(options =>
					options.UseMySql("server=localhost;user=ryan;password=password;database=CMCS-PROG6021",
						new MySqlServerVersion(new Version(10, 4, 32))));

			// Add session services
			builder.Services.AddSession(options =>
					{
					options.IdleTimeout = TimeSpan.FromMinutes(30);
					options.Cookie.HttpOnly = true;
					options.Cookie.IsEssential = false;
					});

			// Configure authentication
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
						{
						options.LoginPath = "/Users/Login";
						options.AccessDeniedPath = "/Account/AccessDenied";
						});

			// Register ReportService as Scoped
			builder.Services.AddScoped<IReportService, ReportService>();

			builder.Services.AddMvc().AddXmlSerializerFormatters();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseSession();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<CookieSessionMiddleware>();
			app.MapRazorPages();

			app.Run();
		}
	}
}
