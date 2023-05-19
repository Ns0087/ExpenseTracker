using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Models.ResponseViewModels;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseModel> logIn(LogInModel logIn);
        public Task<bool> SignUp(SignUpModel user);
    }
}
