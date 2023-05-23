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
        private readonly ICryptographicService cryptographicService;
        private readonly ICategoryService categoryService;

        public CategoryController(IServiceProvider serviceProvider)
        {
            cryptographicService = serviceProvider.GetRequiredService<ICryptographicService>();
            categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        }

        // GET: Category
        public async Task<IActionResult> Index(string? Id)
        {
            if (Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try
            {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }

            var categories = await categoryService.GetAllCategoriesAsync(userId);

            ViewBag.Id = Id;

            return categories != null ?
                        View(categories) :
                        Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        }


        // GET: Category/AddOrEdit
        public async Task<IActionResult> AddOrEdit(string? Id, int categoryId = 0)
        {
            if (Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try
            {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }

            ViewBag.Id = Id;

            if (categoryId == 0)
                return View(new CategoryModel());
            else
            {
                try
                {
                    var category = await categoryService.GetCategoryByIdAsync(categoryId);
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
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] CategoryModel category, [Bind("Id")]string? Id)
        {
            if (ModelState.IsValid)
            {
                if (Id == null)
                {
                    return BadRequest("Not a valid User!!");
                }

                int userId = -1;

                try
                {
                    userId = cryptographicService.Decrypt(Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return BadRequest("Not a valid User!!");
                }

                if (userId <= 0)
                {
                    return BadRequest("Not a valid User!!");
                }

                ViewBag.Id = Id;

                try
                {
                    category.UserId = userId;

                    if (category.CategoryId == 0)
                        await categoryService.AddCategoryAsync(category);
                    else
                        await categoryService.UpdateCategoryAsync(category);

                    return RedirectToAction(nameof(Index), new { Id = ViewBag.Id });
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
        public async Task<IActionResult> DeleteConfirmed(string? Id, int categoryId)
        {
            if (Id == null)
            {
                return BadRequest("Not a valid User!!");
            }

            int userId = -1;

            try
            {
                userId = cryptographicService.Decrypt(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not a valid User!!");
            }

            if (userId <= 0)
            {
                return BadRequest("Not a valid User!!");
            }

            ViewBag.Id = Id;

            try
            {
                await categoryService.DeleteCategoryAsync(categoryId);
                return RedirectToAction(nameof(Index), new { Id = ViewBag.Id});
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
