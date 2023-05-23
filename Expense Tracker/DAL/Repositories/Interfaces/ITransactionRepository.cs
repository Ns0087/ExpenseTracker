using Expense_Tracker.DAL.Entities;

namespace Expense_Tracker.DAL.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetAllTransactionsAsync(int userId);
        public Task<List<Transaction>> GetAllTransactionsForDashboardAsync(int userId);
        public Task<Transaction> GetTransactionByIdAsync(int id);
        public Task<bool> AddTransactionAsync(Transaction transaction);
        public Task<bool> UpdateTransactionAsync(Transaction transaction, Transaction oldTransaction);
        public Task<bool> DeleteTransactionAsync(Transaction transaction);
    }
}
