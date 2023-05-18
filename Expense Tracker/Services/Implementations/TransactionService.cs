using Expense_Tracker.DAL.Entities;
using Expense_Tracker.DAL.Repositories.Implementations;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Expense_Tracker.Extensions;
using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Models.ResponseViewModels;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository transactionRepository;

        public TransactionService(IServiceProvider serviceProvider)
        {
            transactionRepository = serviceProvider.GetRequiredService<ITransactionRepository>();
        }

        public async Task<bool> AddTransactionAsync(TransactionModel transaction)
        {
            try
            {
                return await transactionRepository.AddTransactionAsync(transaction.ToEntity<TransactionModel, Transaction>());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            try
            {
                var transaction = await transactionRepository.GetTransactionByIdAsync(id);
                if (transaction != null)
                {
                    return await transactionRepository.DeleteTransactionAsync(transaction);
                }
                else
                {
                    throw new Exception("Transaction does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TransactionResponseModel>> GetAllTransactionAsync()
        {
            List<TransactionResponseModel> list = new List<TransactionResponseModel>();
            var transactions = await transactionRepository.GetAllTransactionsAsync();

            foreach (var transaction in transactions)
            {
                list.Add(transaction.ToEntity<Transaction, TransactionResponseModel>());
            }

            return list;
        }

        public async Task<List<TransactionResponseModel>> GetSelectedTransactionsAsync()
        {
            List<TransactionResponseModel> list = new List<TransactionResponseModel>();
            var transactions = await transactionRepository.GetAllTransactionsForDashboardAsync();

            foreach (var transaction in transactions)
            {
                list.Add(transaction.ToEntity<Transaction, TransactionResponseModel>());
            }

            return list;
        }

        public async Task<TransactionResponseModel> GetTransactionByIdAsync(int id)
        {
            try
            {
                var transaction = await transactionRepository.GetTransactionByIdAsync(id);
                if (transaction != null)
                {
                    return transaction.ToEntity<Transaction, TransactionResponseModel>();
                }
                else
                {
                    throw new Exception("No Transaction Found!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateTransactionAsync(TransactionModel transaction)
        {
            try
            {
                if (transaction == null)
                {
                    throw new Exception("Please enter valid data!!");
                }
                else
                {
                    var oldTransaction = await transactionRepository.GetTransactionByIdAsync(transaction.TransactionId);
                    if (oldTransaction != null)
                    {
                        return await transactionRepository.UpdateTransactionAsync(transaction.ToEntity<TransactionModel, Transaction>(), oldTransaction);
                    }
                    else
                    {
                        throw new Exception("Transaction does not exist!!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
