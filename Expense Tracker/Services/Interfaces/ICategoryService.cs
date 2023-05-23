using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Models.ResponseViewModels;

namespace Expense_Tracker.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryResponseModel>> GetAllCategoriesAsync(int userId);
        public Task<CategoryResponseModel> GetCategoryByIdAsync(int id);
        public Task<bool> AddCategoryAsync(CategoryModel category);
        public Task<bool> UpdateCategoryAsync(CategoryModel category);
        public Task<bool> DeleteCategoryAsync(int id);
    }
}
