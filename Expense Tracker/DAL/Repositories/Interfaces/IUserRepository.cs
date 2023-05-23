using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.DAL.Entities;

namespace Expense_Tracker.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsync(LogInModel logIn);
        public Task<bool> AddUserAsync(User user);
        public Task<bool> DeleteUserAsync(int userId);
    }
}
