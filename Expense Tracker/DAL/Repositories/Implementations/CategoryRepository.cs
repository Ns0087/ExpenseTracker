using Expense_Tracker.DAL.Entities;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DAL.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(IServiceProvider serviceProvider)
        {
            dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }
        public async Task<bool> AddCategoryAsync(Category category)
        {
            try
            {
                await dbContext.Categories.AddAsync(category);
                return (await dbContext.SaveChangesAsync()) > 0;
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            try
            {
                dbContext.Remove(category);
                return (await dbContext.SaveChangesAsync()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync(int userId)
        {
            try
            {
                var list = await dbContext.Categories.ToListAsync();
                return list.Where(li => li.UserId == userId || li.UserId == 0).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateCategoryAsync(Category category, Category oldCategory)
        {
            try
            {
                dbContext.Entry<Category>(oldCategory).CurrentValues.SetValues(category);
                return (await dbContext.SaveChangesAsync()) > 0;
            }catch(Exception)
            {
                throw;
            }
        }
    }
}
