using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;
using Expense_Tracker.Services.Interfaces;
using Expense_Tracker.Models.RequestViewModels;
using Expense_Tracker.Extensions;
using Expense_Tracker.Models.ResponseViewModels;

namespace Expense_Tracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly ICategoryService categoryService;
        public TransactionController(IServiceProvider serviceProvider)
        {
            transactionService = serviceProvider.GetRequiredService<ITransactionService>();
            categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await transactionService.GetAllTransactionAsync());
        }

        // GET: Transaction/AddOrEdit
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            await PopulateCategories();
            if (id == 0)
                return View(new TransactionModel());
            else
            {
                var transaction = await transactionService.GetTransactionByIdAsync(id);
                return View(transaction.ToEntity<TransactionResponseModel, TransactionModel>());
            }
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] TransactionModel transaction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (transaction.TransactionId == 0)
                    {
                        await transactionService.AddTransactionAsync(transaction);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        await transactionService.UpdateTransactionAsync(transaction);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }
            await PopulateCategories();
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await transactionService.DeleteTransactionAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [NonAction]
        public async Task PopulateCategories()
        {
            var CategoryCollection = await categoryService.GetAllCategoriesAsync();
            var DefaultCategory = new CategoryResponseModel() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory);

            var newCategoryCollection = new List<CategoryModel>();
            foreach (var category in CategoryCollection)
            {
                newCategoryCollection.Add(category.ToEntity<CategoryResponseModel, CategoryModel>());
            }

            ViewBag.Categories = newCategoryCollection;

        }
    }
}
