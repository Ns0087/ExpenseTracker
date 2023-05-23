using Expense_Tracker.Models;
using Expense_Tracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardService dashboardService;
        private readonly ICryptographicService cryptographicService;

        public DashboardController(IServiceProvider serviceProvider)
        {
            dashboardService = serviceProvider.GetRequiredService<IDashboardService>();
            cryptographicService = serviceProvider.GetRequiredService<ICryptographicService>();
        }

        public async Task<ActionResult> Index(string? Id)
        {
            if(Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }
            
            var dashboard = await dashboardService.GetDashboardDetailsAsync(userId);

            //Total Income
            ViewBag.TotalIncome = dashboard.TotalIncome.ToString("C0");

            //Total Expense
            ViewBag.TotalExpense = dashboard.TotalExpense.ToString("C0");

            //Balance
            int Balance = dashboard.Balance;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            //Doughnut Chart - Expense By Category
            ViewBag.DoughnutChartData = dashboard.DoughnutChartData;

            //Spline Chart - Income vs Expense

            //Income
            ViewBag.SplineChartData = dashboard.SplineChartData;
            //Recent Transactions
            ViewBag.RecentTransactions = dashboard.RecentTransactions;

            //UserId
            ViewBag.Id = Id;

            return View();
        }
    }
}
