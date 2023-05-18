using Expense_Tracker.Models.ResponseViewModels;
using Expense_Tracker.Models.RequestViewModels;

namespace Expense_Tracker.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<List<TransactionResponseModel>> GetAllTransactionAsync();
        public Task<List<TransactionResponseModel>> GetSelectedTransactionsAsync();
        public Task<TransactionResponseModel> GetTransactionByIdAsync(int id);
        public Task<bool> AddTransactionAsync(TransactionModel transaction);
        public Task<bool> UpdateTransactionAsync(TransactionModel transaction);
        public Task<bool> DeleteTransactionAsync(int id);
    }
}
