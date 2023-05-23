using Expense_Tracker.DAL.Entities;
using Expense_Tracker.Models.RequestViewModels;

namespace Expense_Tracker.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategoriesAsync(int userId);
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task<bool> AddCategoryAsync(Category category);
        public Task<bool> UpdateCategoryAsync(Category category, Category oldCategory);
        public Task<bool> DeleteCategoryAsync(Category category);
    }
}
