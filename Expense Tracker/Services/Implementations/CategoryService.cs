using Expense_Tracker.DAL.Entities;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Expense_Tracker.Extensions;
using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Models.ResponseViewModels;
using Expense_Tracker.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Expense_Tracker.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(IServiceProvider serviceProvider)
        {
            categoryRepository = serviceProvider.GetRequiredService<ICategoryRepository>();
        }

        public async Task<List<CategoryResponseModel>> GetAllCategoriesAsync(int userId)
        {
            List<CategoryResponseModel> list = new List<CategoryResponseModel>();
            var categories = await categoryRepository.GetAllCategoriesAsync(userId);
            foreach (var category in categories)
            {
                list.Add(category.ToEntity<Category, CategoryResponseModel>());
            }

            return list;
        }

        public async Task<CategoryResponseModel> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await categoryRepository.GetCategoryByIdAsync(id);
                if (category != null)
                {
                    return category.ToEntity<Category, CategoryResponseModel>();
                }
                else
                {
                    throw new Exception("No Category Found!!");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddCategoryAsync(CategoryModel category)
        {
            try
            {
                return await categoryRepository.AddCategoryAsync(category.ToEntity<CategoryModel, Category>());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCategoryAsync(CategoryModel category)
        {
            try
            {
                if (category == null)
                {
                    throw new Exception("Please enter valid data!!");
                }
                else
                {
                    var oldCategory = await categoryRepository.GetCategoryByIdAsync(category.CategoryId);
                    if(oldCategory != null)
                    {
                       return await categoryRepository.UpdateCategoryAsync(category.ToEntity<CategoryModel, Category>(), oldCategory);
                    }
                    else
                    {
                        throw new Exception("Category does not exist!!");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await categoryRepository.GetCategoryByIdAsync(id);
                if(category != null)
                {
                    return await categoryRepository.DeleteCategoryAsync(category);
                }
                else
                {
                    throw new Exception("Category does not exist!!");
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
