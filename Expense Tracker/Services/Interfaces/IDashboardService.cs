using Expense_Tracker.Models.ResponseViewModels;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IDashboardService
    {
        public Task<DashboardResponseModel> GetDashboardDetailsAsync();

    }
}
