using Expense_Tracker.DAL.Entities;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Expense_Tracker.Extensions;
using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Models.ResponseViewModels;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IServiceProvider serviceProvider)
        {
            userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        }

        public async Task<UserResponseModel> logIn(LogInModel logIn)
        {
            try
            {
                var user = await userRepository.GetUserAsync(logIn);
                if (user == null)
                {
                    throw new Exception("Invalid Login Credentials!!");
                }
                else
                {
                    return user.ToEntity<User, UserResponseModel>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SignUp(SignUpModel user)
        {
            try
            {
                if(await userRepository.AddUserAsync(user.ToEntity<SignUpModel, User>()))
                {
                    return true;
                }
                else
                {
                    throw new Exception("Something went wrong, Please try again later!!");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
