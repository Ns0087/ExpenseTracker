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

        public DashboardController(IServiceProvider serviceProvider)
        {
            dashboardService = serviceProvider.GetRequiredService<IDashboardService>();
        }

        public async Task<ActionResult> Index()
        {
            var dashboard = await dashboardService.GetDashboardDetailsAsync();

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

            return View();
        }
    }
}
