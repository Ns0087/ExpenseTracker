using Expense_Tracker.Models.ResponseViewModels;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly ITransactionService transactionService;

        public DashboardService(IServiceProvider serviceProvider)
        {
            transactionService = serviceProvider.GetRequiredService<ITransactionService>();
        }

        public async Task<DashboardResponseModel> GetDashboardDetailsAsync()
        {
            try
            {
                var SelectedTransactions = new List<TransactionResponseModel>();
                SelectedTransactions = await transactionService.GetSelectedTransactionsAsync();
                var dashBoard = await MapDashboardDetailsAsync(SelectedTransactions);
                return dashBoard;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<DashboardResponseModel> MapDashboardDetailsAsync(List<TransactionResponseModel> selectedTransactions)
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            var dashboard = new DashboardResponseModel();
            dashboard.TotalIncome = selectedTransactions.Where(i => i.Category.Type == "Income").Sum(j => j.Amount);
            dashboard.TotalExpense = selectedTransactions.Where(i => i.Category.Type == "Expense").Sum(j => j.Amount);
            dashboard.Balance = dashboard.TotalIncome - dashboard.TotalExpense;
            dashboard.DoughnutChartData = GetDoughnutChartData(selectedTransactions);
            dashboard.SplineChartData = GetSplineChartData(selectedTransactions, StartDate, EndDate);
            dashboard.RecentTransactions = (await transactionService.GetAllTransactionAsync()).OrderByDescending(j => j.Date).Take(5).ToList();
            return dashboard;
        }

        private List<DoughnutChartData> GetDoughnutChartData(List<TransactionResponseModel> selectedTransactions)
        {
            var doughnutChartData = selectedTransactions.Where(i => i.Category.Type == "Expense").GroupBy(j => j.Category.CategoryId)
                                                              .Select(k => new DoughnutChartData()
                                                              {
                                                                  categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                                                                  amount = k.Sum(j => j.Amount),
                                                                  formattedAmount = k.Sum(j => j.Amount).ToString("C0"),
                                                              })
                                                              .OrderByDescending(l => l.amount).ToList();

            return doughnutChartData;
        }

        private IEnumerable<SplineChartData> GetSplineChartData(List<TransactionResponseModel> selectedTransactions, DateTime StartDate, DateTime EndDate)
        {
            //Income
            List<SplineChartData> IncomeSummary = selectedTransactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    income = k.Sum(l => l.Amount)
                })
                .ToList();

            //Expense
            List<SplineChartData> ExpenseSummary = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    expense = k.Sum(l => l.Amount)
                })
                .ToList();

            //Combine Income & Expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            var splineChartData = (from day in Last7Days
                                   join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                   from income in dayIncomeJoined.DefaultIfEmpty()
                                   join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                   from expense in expenseJoined.DefaultIfEmpty()
                                   select new SplineChartData()
                                   {
                                       day = day,
                                       income = income == null ? 0 : income.income,
                                       expense = expense == null ? 0 : expense.expense,
                                   });

            return splineChartData;
        }
    }
}
