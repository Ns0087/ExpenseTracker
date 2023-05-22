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

        public async Task<User> GetUserAsync(LogInModel logIn)
        {
            try
            {
                var list = await dbContext.Users.ToListAsync();
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == logIn.Email && u.Password == logIn.Password);
                return user;
            }catch (Exception)
            {
                throw;
            }
        }
    }
}
