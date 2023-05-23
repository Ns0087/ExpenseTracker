using Expense_Tracker.DAL.Entities;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DAL.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TransactionRepository(IServiceProvider serviceProvider)
        {
            dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }

        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                await dbContext.AddAsync(transaction);
                return (await dbContext.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteTransactionAsync(Transaction transaction)
        {
            try
            {
                dbContext.Remove(transaction);
                return (await dbContext.SaveChangesAsync()) > 0;
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(int userId)
        {
            try
            {
                var list = await dbContext.Transactions.Include(t => t.Category).ToListAsync();
                return list.Where(li => li.UserId == userId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Transaction>> GetAllTransactionsForDashboardAsync(int userId)
        {
            try
            {
                DateTime StartDate = DateTime.Today.AddDays(-6);
                DateTime EndDate = DateTime.Today;
                List<Transaction> SelectedTransactions = await dbContext.Transactions
               .Include(x => x.Category)
               .Where(y => y.Date >= StartDate && y.Date <= EndDate && y.UserId == userId)
               .ToListAsync();

                return SelectedTransactions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            try
            {
                return await dbContext.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction, Transaction oldTransaction)
        {
            try
            {
                dbContext.Entry<Transaction>(oldTransaction).CurrentValues.SetValues(transaction);
                return (await dbContext.SaveChangesAsync()) > 0;
            }catch (Exception)
            {
                throw;
            }
        }
    }
}
