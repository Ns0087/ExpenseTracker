using Expense_Tracker.DAL.Entities;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Expense_Tracker.Models.RequestViewModels;

namespace Expense_Tracker.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public Task<int> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(LogInModel logIn)
        {
            throw new NotImplementedException();
        }
    }
}
