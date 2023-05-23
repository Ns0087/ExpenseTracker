using Expense_Tracker.DAL.Entities;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Expense_Tracker.Models.RequestViewModels;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(IServiceProvider serviceProvider)
        {
            dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await dbContext.Users.AddAsync(user);
                return (await dbContext.SaveChangesAsync() > 0);
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var oldUser = await GetUserByIdAsync(userId);
                if (oldUser != null)
                {
                    var newUser = new User()
                    {
                        UserId = userId,
                        FullName = oldUser.FullName,
                        UserName = oldUser.UserName,
                        Email = oldUser.Email,
                        Phone = oldUser.Phone,
                        Password = oldUser.Password,
                        Gender = oldUser.Gender,
                        IsDeleted = true
                    };

                    dbContext.Entry<User>(oldUser).CurrentValues.SetValues(newUser);
                    return (await dbContext.SaveChangesAsync() > 0);
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserAsync(LogInModel logIn)
        {
            try
            {
                //var list = await dbContext.Users.ToListAsync();
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == logIn.Email && u.Password == logIn.Password && u.IsDeleted == false);
                return user;
            }catch (Exception)
            {
                throw;
            }
        }

        private async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId && u.IsDeleted == false);
            return user;
        }
    }
}
