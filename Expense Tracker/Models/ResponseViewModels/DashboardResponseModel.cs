using Expense_Tracker.Controllers;

namespace Expense_Tracker.Models.ResponseViewModels
{
    public class DashboardResponseModel
    {
        public int TotalIncome { get; set; }
        public int TotalExpense { get; set; }
        public int Balance { get; set; }
        public List<DoughnutChartData>? DoughnutChartData { get; set; }
        public IEnumerable<SplineChartData>? SplineChartData { get; set; }
        public List<TransactionResponseModel>? RecentTransactions { get; set; }
    }
}
