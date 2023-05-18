using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;
using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.DAL;
using Expense_Tracker.DAL.Repositories.Interfaces;
using Expense_Tracker.Services.Interfaces;
using Expense_Tracker.Extensions;
using Expense_Tracker.Models.ResponseViewModels;

namespace Expense_Tracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return categories != null ?
                        View(categories) :
                        Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        }


        // GET: Category/AddOrEdit
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new CategoryModel());
            else
            {
                try
                {
                    var category = await categoryService.GetCategoryByIdAsync(id);
                    return View(category.ToEntity<CategoryResponseModel, CategoryModel>());
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }

        }

        // POST: Category/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (category.CategoryId == 0)
                        await categoryService.AddCategoryAsync(category);
                    else
                        await categoryService.UpdateCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }
            return View(category);
        }


        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await categoryService.DeleteCategoryAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
